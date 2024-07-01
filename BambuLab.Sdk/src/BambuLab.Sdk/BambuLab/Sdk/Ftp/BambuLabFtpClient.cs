using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentFTP;

namespace BambuLab.Sdk.Ftp;

public class BambuLabFtpClient
{
    protected string ServerIp { get; }
    protected int Port { get; }
    protected string User { get; }
    protected string AccessCode { get; }
    protected FtpClient FtpClient { get; set; }

    public BambuLabFtpClient(string serverIp, string accessCode, string user = "bblp", int port = 990)
    {
        ServerIp = serverIp;
        Port = port;
        User = user;
        AccessCode = accessCode;
        InitializeClient();
    }

    private void InitializeClient()
    {
        FtpClient = new FtpClient(ServerIp, Port)
        {
            Credentials = new NetworkCredential(User, AccessCode),
            Config = new FtpConfig()
            {
                EncryptionMode = FtpEncryptionMode.Implicit,
                DataConnectionType = FtpDataConnectionType.PASV,
                ValidateAnyCertificate = true
            }
        };
    }

    private Task ConnectAsync()
    {
        FtpClient.Connect();
        return Task.CompletedTask;
    }

    private Task DisconnectAsync()
    {
        FtpClient.Disconnect();
        return Task.CompletedTask;
    }

    private async Task<T> ConnectAndRunAsync<T>(Func<Task<T>> func)
    {
        try
        {
            await ConnectAsync();
            return await func();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await DisconnectAsync();
        }
    }

    public Task<string> UploadFileAsync(Stream fileStream, string filePath)
    {
        return ConnectAndRunAsync(() =>
        {
            FtpClient.UploadStream(fileStream, filePath);
            return Task.FromResult(filePath);
        });
    }

    public Task<string> DeleteFileAsync(string filePath)
    {
        return ConnectAndRunAsync(() =>
        {
            FtpClient.DeleteFile(filePath);
            return Task.FromResult(filePath);
        });
    }

    public void Close()
    {
        FtpClient.Dispose();
    }
}
