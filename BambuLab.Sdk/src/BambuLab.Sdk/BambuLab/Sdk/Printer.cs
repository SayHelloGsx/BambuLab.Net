using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BambuLab.Sdk.Ftp;
using BambuLab.Sdk.Mqtt;

namespace BambuLab.Sdk;

public abstract class Printer : IPrinter
{
    protected virtual string IpAddress { get; }
    protected virtual string AccessCode { get; }
    protected virtual string Serial { get; }
    protected virtual string UserName { get; }
    protected virtual BambuLabMqttClient BambuLabMqttClient { get; }
    protected virtual BambuLabFtpClient BambuLabFtpClient { get; }

    protected virtual PrintStatusEnum LastPrintStatus { get; set; }

    public event Func<PrintStatusEnum, Task> OnPrintStatusChanged;

    public bool IsMqttConnected => BambuLabMqttClient.IsConnected;

    public Printer(string ipAddress, string accessCode, string serial, string userName)
    {
        IpAddress = ipAddress;
        AccessCode = accessCode;
        Serial = serial;
        UserName = userName;

        BambuLabMqttClient = new BambuLabMqttClient(ipAddress, accessCode, serial, userName)
            .RegisterDataUpdatedEvent(OnDataUpdateAsync);

        BambuLabFtpClient = new BambuLabFtpClient(ipAddress, accessCode);
    }

    public virtual async Task ConnectAsync()
    {
        await BambuLabMqttClient.ConnectAsync();
    }

    public virtual async Task DisconnectAsync()
    {
        await BambuLabMqttClient.DisconnectAsync();
    }

    public Printer RegisterOnPrintStatusChangedAsync(Func<PrintStatusEnum, Task> func)
    {
        OnPrintStatusChanged += func;
        return this;
    }

    #region Get
    public virtual async Task<int?> GetRemainingTimeAsync()
    {
        return await BambuLabMqttClient.GetAsync<int?>("mc_remaining_time");
    }

    public virtual async Task<int?> GetPercentageAsync()
    {
        return await BambuLabMqttClient.GetAsync<int?>("mc_percent");
    }

    public virtual async Task<GcodeStateEnum> GetPrinterStateAsync()
    {
        return await BambuLabMqttClient.FindAsync("gcode_state", GcodeStateEnum.UNKNOWN);
    }

    public virtual async Task<int?> GetPrintSpeedAsync()
    {
        return await BambuLabMqttClient.GetAsync<int?>("spd_mag");
    }

    public virtual async Task<float> GetBedTemperature()
    {
        return await BambuLabMqttClient.FindAsync("bed_temper", 0.0f);
    }

    public virtual async Task<float> GetNozzleTemperatureAsync()
    {
        return await BambuLabMqttClient.FindAsync("nozzle_temper", 0.0f);
    }

    public virtual async Task<float?> GetNozzleTemperatureTargetAsync()
    {
        return await BambuLabMqttClient.FindAsync<float?>("nozzle_target_temper", null);
    }

    public virtual async Task<int> CurrentLayerNumAsync()
    {
        return await BambuLabMqttClient.GetAsync<int>("layer_num");
    }

    public virtual async Task<int> TotalLayerNumAsync()
    {
        return await BambuLabMqttClient.GetAsync<int>("total_layer_num");
    }

    public virtual async Task<int?> GetGcodeFilePreparePercentageAsync()
    {
        return await BambuLabMqttClient.FindAsync<int?>("gcode_file_prepare_percent", null);
    }

    public virtual async Task<float> GetNozzleDiameterAsync()
    {
        return await BambuLabMqttClient.GetAsync<float>("nozzle_diameter");
    }

    public virtual async Task<NozzleTypeEnum> GetNozzleTypeAsync()
    {
        return await BambuLabMqttClient.GetAsync<NozzleTypeEnum>("nozzle_type");
    }

    public virtual async Task<string> GetFileNameAsync()
    {
        return await BambuLabMqttClient.FindAsync<string>("gcode_file", null);
    }

    public virtual async Task<LightStateEnum> GetLightStateAsync()
    {
        var lightsReport = await BambuLabMqttClient.GetAsync<List<Dictionary<string, string>>>("lights_report");

        if (lightsReport == null || lightsReport.Count == 0)
        {
            return LightStateEnum.unknow;
        }

        return lightsReport[0].ContainsKey("mode") ? lightsReport[0]["mode"].GetEnumValueOrDefault(LightStateEnum.unknow) : LightStateEnum.unknow;
    }

    public virtual async Task<PrintStatusEnum> GetCurrentStateAsync()
    {
        return await BambuLabMqttClient.GetAsync<PrintStatusEnum>("stg_cur");
    }
    #endregion

    #region Publish
    /// <summary>
    /// Turn the light on
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> TurnLightOnAsync()
    {
        return await BambuLabMqttClient.PublishCommandAsync(Commands.TurnLightOn);
    }

    /// <summary>
    /// Turn the light off
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> TurnLightOffAsync()
    {
        return await BambuLabMqttClient.PublishCommandAsync(Commands.TurnLightOff);
    }

    public virtual async Task<bool> StartPrintAsync(string filename, int plateNumber, bool useAms = true, List<int> amsMapping = null)
    {
        if (amsMapping == null)
        {
            amsMapping = new List<int> { 0 };
        }

        var command = new Dictionary<string, object>
        {
            { "print", new Dictionary<string, object>
                {
                    { "command", "project_file" },
                    { "param", $"Metadata/plate_{plateNumber}.gcode" },
                    { "subtask_name", filename },
                    { "bed_leveling", true },
                    { "flow_calibration", true },
                    { "vibration_calibration", true },
                    { "url", $"ftp://{filename}" },
                    { "layer_inspect", false },
                    { "use_ams", useAms },
                    { "ams_mapping", amsMapping }
                }
            }
        };

        return await BambuLabMqttClient.PublishCommandAsync(command);
    }

    /// <summary>
    /// Pause print
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> PausePrintAsync()
    {
        if (await GetPrinterStateAsync() == GcodeStateEnum.PAUSE)
        {
            return true;
        }

        return await BambuLabMqttClient.PublishCommandAsync(Commands.Pause);
    }

    /// <summary>
    /// Resume print
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> ResumePrintAsync()
    {
        if (await GetPrinterStateAsync() == GcodeStateEnum.RUNNING)
        {
            return true;
        }

        return await BambuLabMqttClient.PublishCommandAsync(Commands.Resume);
    }

    /// <summary>
    /// Stop print
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> StopPrintAsync()
    {
        return await BambuLabMqttClient.PublishCommandAsync(Commands.Stop);
    }

    public virtual async Task<bool> ResumeFilamentActionAsync()
    {
        return await BambuLabMqttClient.PublishCommandAsync(Commands.ResumeFilamentAction);
    }

    /// <summary>
    /// Set bed temperature
    /// </summary>
    /// <param name="temperature"></param>
    /// <returns></returns>
    public virtual async Task<bool> SetBedTemperatureAsync(int temperature)
    {
        return await BambuLabMqttClient.SendGcodeLineAsync($"M140 S{temperature}\n");
    }

    /// <summary>
    /// Set printer to home
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> HomePrinterAsync()
    {
        return await BambuLabMqttClient.SendGcodeLineAsync("G28\n");
    }

    public virtual async Task<bool> SetPrintSpeedAsync(PrintSpeedEnum speedLevel)
    {
        return await BambuLabMqttClient.PublishCommandAsync(Commands.SetPrintSpeedLvl, speedLevel);
    }

    //public virtual async Task<bool> LoadFilamentSpoolAsync()
    //{
    //    return await BambuLabMQTTClient.PublishCommandAsync(Commands.LoadFilamentSpool);
    //}

    //public virtual async Task<bool> UnloadFilamentSpoolAsync()
    //{
    //    return await BambuLabMQTTClient.PublishCommandAsync(Commands.UnloadFilamentSpool);
    //}

    //public virtual async Task<bool> SetPrinterFilamentAsync(FilamentEnum filamentMaterial, string colour)
    //{
    //    if (colour.Length != 6)
    //    {
    //        throw new ArgumentException("Colour must be a 6 character hex string");
    //    }

    //    var filamentInfo = filamentMaterial.GetAttribute<FilamentInfoAttribute>();

    //    return await BambuLabMQTTClient.PublishCommandAsync(new Dictionary<string, object>
    //    {
    //        {
    //            "print", new Dictionary<string, object>
    //            {
    //                { "command", "ams_filament_setting" },
    //                { "ams_id", 255 },
    //                { "tray_id", 254 },
    //                { "tray_info_idx", filamentInfo.TrayInfoIdx },
    //                { "tray_color", $"{colour.ToUpper()}FF" },
    //                { "nozzle_temp_min", filamentInfo.NozzleTempMin },
    //                { "nozzle_temp_max", filamentInfo.NozzleTempMax },
    //                { "tray_type", filamentInfo.TrayType }
    //            }
    //        }
    //    });
    //}

    //public virtual async Task<bool> MoveZAxisAsync(int height)
    //{
    //    return await BambuLabMQTTClient.SendGcodeLineAsync($"G90\nG0 Z{height}\n");
    //}

    //public virtual async Task<bool> CalibrateAsync(bool bedLevelling = true, bool motorNoiseCancellation = true, bool vibrationCompensation = true)
    //{
    //    int bitmask = 0;

    //    if (bedLevelling) bitmask |= 1 << 1;
    //    if (vibrationCompensation) bitmask |= 1 << 2;
    //    if (motorNoiseCancellation) bitmask |= 1 << 3;

    //    return await BambuLabMQTTClient.PublishCommandAsync(Commands.Calibrate, bitmask);
    //}
    #endregion

    protected virtual async Task OnDataUpdateAsync(Dictionary<string, string> data)
    {
        var currentStatus = await GetCurrentStateAsync();

        if (LastPrintStatus == currentStatus)
        {
            return;
        }

        LastPrintStatus = currentStatus;

        if (null != OnPrintStatusChanged)
        {
            await OnPrintStatusChanged.Invoke(currentStatus);
        }
    }

    public virtual async Task<string> DeleteFileAsync(string filePath)
    {
        return await BambuLabFtpClient.DeleteFileAsync(filePath);
    }

    public virtual async Task<string> UploadFileAsync(Stream file, string filename = "ftp_upload.gcode")
    {
        return await BambuLabFtpClient.UploadFileAsync(file, filename);
    }

    public virtual Task<string> GetCameraFrameAsync()
    {
        throw new NotImplementedException();
    }

    public async ValueTask DisposeAsync()
    {
        await BambuLabMqttClient.DisconnectAsync();
    }
}
