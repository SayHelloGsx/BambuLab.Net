using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Linq;
using System.IO;
using System.Collections;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using BambuLab.Sdk;
using BambuLab.Sdk.Exceptions;
using BambuLab.Sdk.Json;

namespace BambuLab.Sdk.Mqtt;

public class BambuLabMqttClient
{
    protected virtual string Hostname { get; }
    protected virtual string Access { get; }
    protected virtual string Username { get; }
    protected virtual string PrinterSerial { get; }
    protected virtual int Port { get; }
    protected virtual int Timeout { get; }
    protected virtual IMqttClient Client { get; }
    protected virtual MqttClientOptions Options { get; }
    protected virtual Dictionary<int, AMS> Ams { get; }
    protected virtual string CommandTopic { get; }
    protected virtual Dictionary<string, string> Data { get; }
    protected virtual int LastUpdate { get; set; }
    protected virtual IJsonSerializer JsonSerializer => new DefaultJsonSerializer();

    protected const int PrinterTimeout = 10;

    protected Dictionary<string, Func<string, string, Task>> SubscriptionKeyMap { get; } = new Dictionary<string, Func<string, string, Task>>();

    public event Func<Dictionary<string, string>, Task> OnDataUpdatedAsync;

    public event Func<Task> OnDisconnectedAsync;

    public bool IsConnected => Client.IsConnected;

    public BambuLabMqttClient(
        string hostname,
        string access,
        string printerSerial,
        string username,
        int port = 8883,
        int timeout = 60)
    {
        Hostname = hostname;
        Access = access;
        Username = username;
        PrinterSerial = printerSerial;
        Port = port;
        Timeout = timeout;
        LastUpdate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Data = new Dictionary<string, string>();
        Ams = new Dictionary<int, AMS>();
        CommandTopic = $"device/{printerSerial}/request";

        var factory = new MqttFactory();
        Client = factory.CreateMqttClient();

        Options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithCredentials(username, access)
            .WithTcpServer(hostname, port)
            .WithTlsOptions(new MqttClientTlsOptions
            {
                UseTls = true,
                SslProtocol = SslProtocols.Tls12,
                CertificateValidationHandler = context => true,
            })
            .Build();

        Client.ConnectedAsync += OnConnectAsync;
        Client.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
        Client.DisconnectedAsync += OnDisconnectedAsnyc;
    }

    protected virtual async Task OnDisconnectedAsnyc(MqttClientDisconnectedEventArgs args)
    {
        if (null != OnDisconnectedAsync)
        {
            await OnDisconnectedAsync.Invoke();
        }
    }

    public virtual async Task ConnectAsync()
    {
        await Client.ConnectAsync(Options);
    }

    public virtual async Task DisconnectAsync()
    {
        await Client.DisconnectAsync();
    }

    public virtual async Task GetAmsFilamentAsync()
    {
        var amsInfo = await FindAsync("ams", "{}");
        var amsData = await JsonSerializer.DeserializeAsync<Dictionary<string, object>>(amsInfo.ToString());

        if (amsData == null || amsData.TryGetValue("ams_exist_bits", out var amsExistBits) && amsExistBits.ToString() == "0")
        {
            return;
        }

        var amsDataStream = new MemoryStream(Encoding.UTF8.GetBytes(amsData["ams"].ToString()));
        var amsUnits = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Dictionary<string, object>>>(amsDataStream);
        amsDataStream.Close();

        if (amsUnits == null || amsUnits.Count <= 0)
        {
            return;
        }

        foreach (var amsUnit in amsUnits)
        {
            amsUnit.TryGetValue("humidity", out var humidityObject);
            var humidity = humidityObject?.ToString();

            amsUnit.TryGetValue("temp", out var temperatureObject);
            var temperature = temperatureObject == null ? 0.0f : Convert.ToSingle(temperatureObject);

            amsUnit.TryGetValue("id", out var idObject);
            var id = idObject == null ? 0 : Convert.ToInt32(idObject);

            var ams = new AMS(humidity?.ToString(), temperature);

            if (amsUnit.TryGetValue("tray", out var traysObj) && traysObj is List<Dictionary<string, object>> trays)
            {
                foreach (var tray in trays)
                {
                    tray.TryGetValue("id", out var trayIdObject);
                    var trayId = Convert.ToInt32(trayIdObject);
                    var filamentTray = FilamentTray.FromDictionary(tray);

                    ams.SetFilamentTray(filamentTray, trayId);
                }
            }

            Ams[id] = ams;
        }
    }

    public virtual async Task<bool> SendGcodeLineAsync(string gcodeCommand)
    {
        return await PublishCommandAsync(new Dictionary<string, object>
        {
            { "print", new Dictionary<string, string> { { "sequence_id", "0" }, { "command", "gcode_line" }, { "param", gcodeCommand } } }
        });
    }

    public virtual async Task<bool> PublishCommandAsync(string payload)
    {
        if (!Client.IsConnected)
        {
            throw new BambuLabMQTTException(ErrorCodes.MQTTClient.ClientIsNotConnected);
        }

        var result = await Client.PublishStringAsync(CommandTopic, payload);
        return result.IsSuccess;
    }

    public virtual async Task<bool> PublishCommandAsync(string payload, params object[] args)
    {
        if (!Client.IsConnected)
        {
            throw new BambuLabMQTTException(ErrorCodes.MQTTClient.ClientIsNotConnected);
        }

        var command = string.Format(payload, args);
        var result = await Client.PublishStringAsync(CommandTopic, command);
        return result.IsSuccess;
    }

    public virtual async Task<bool> PublishCommandAsync(Dictionary<string, object> payload)
    {
        if (!Client.IsConnected)
        {
            throw new BambuLabMQTTException(ErrorCodes.MQTTClient.ClientIsNotConnected);
        }

        var json = await JsonSerializer.SerializeAsync(payload);
        var result = await Client.PublishStringAsync(CommandTopic, json);
        return result.IsSuccess;
    }

    public virtual async Task<T> GetAsync<T>(string key)
    {
        await ManualUpdateAsync();

        if (!Data.TryGetValue(key, out var value))
        {
            throw new BambuLabMQTTException(ErrorCodes.MQTTClient.CannotFindKeyFromData).WithData("KeyName", key);
        }

        return await ParseAsync<T>(value);
    }

    public virtual async Task<T> FindAsync<T>(string key, T defaultValue = default)
    {
        await ManualUpdateAsync();

        if (!Data.TryGetValue(key, out var value))
        {
            return defaultValue;
        }

        return await ParseAsync<T>(value);
    }

    public virtual BambuLabMqttClient SubscribeData(string key, Func<string, string, Task> func)
    {
        SubscriptionKeyMap.Add(key, func);
        return this;
    }

    public virtual BambuLabMqttClient RegisterDataUpdatedEvent(Func<Dictionary<string, string>, Task> func)
    {
        OnDataUpdatedAsync += func;
        return this;
    }

    protected virtual async Task<bool> ManualUpdateAsync()
    {
        if (LastUpdate + PrinterTimeout < (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        {
            return false;
        }

        return await PublishCommandAsync(new Dictionary<string, object> { { "pushing", new Dictionary<string, string> { { "command", "pushall" } } } });
    }


    protected virtual async Task<T> ParseAsync<T>(string value)
    {
        var isNullableType = null != Nullable.GetUnderlyingType(typeof(T));

        if ((isNullableType || typeof(T).IsClass) && value.Equals("null", StringComparison.OrdinalIgnoreCase))
        {
            return default;
        }

        Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        if (targetType.IsEnum)
        {
            var enumValue = Enum.Parse(typeof(T), value);
            return (T)enumValue;
        }

        return targetType switch
        {
            Type t when t == typeof(string) => (T)(object)value,
            Type t when t == typeof(int) && int.TryParse(value, out int intValue) => (T)(object)intValue,
            Type t when t == typeof(uint) && uint.TryParse(value, out uint uintValue) => (T)(object)uintValue,
            Type t when t == typeof(long) && long.TryParse(value, out long longValue) => (T)(object)longValue,
            Type t when t == typeof(ulong) && ulong.TryParse(value, out ulong ulongValue) => (T)(object)ulongValue,
            Type t when t == typeof(short) && short.TryParse(value, out short shortValue) => (T)(object)shortValue,
            Type t when t == typeof(ushort) && ushort.TryParse(value, out ushort ushortValue) => (T)(object)ushortValue,
            Type t when t == typeof(byte) && byte.TryParse(value, out byte byteValue) => (T)(object)byteValue,
            Type t when t == typeof(sbyte) && sbyte.TryParse(value, out sbyte sbyteValue) => (T)(object)sbyteValue,
            Type t when t == typeof(float) && float.TryParse(value, out float floatValue) => (T)(object)floatValue,
            Type t when t == typeof(double) && double.TryParse(value, out double doubleValue) => (T)(object)doubleValue,
            Type t when t == typeof(decimal) && decimal.TryParse(value, out decimal decimalValue) => (T)(object)decimalValue,
            Type t when t == typeof(bool) && bool.TryParse(value, out bool boolValue) => (T)(object)boolValue,
            Type t when t == typeof(char) && char.TryParse(value, out char charValue) => (T)(object)charValue,
            _ => await JsonSerializer.DeserializeAsync<T>(value)
        };
    }

    protected virtual async Task OnConnectAsync(MqttClientConnectedEventArgs e)
    {
        await Client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic($"device/{PrinterSerial}/report").Build());
    }

    protected virtual async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        var message = e.ApplicationMessage;
        var payload = message.PayloadSegment.ToArray();

        if (payload == null || payload.Length == 0)
        {
            return;
        }

        var doc = await JsonSerializer.DeserializeAsync<Dictionary<string, Dictionary<string, object>>>(Encoding.UTF8.GetString(payload));

        if (doc.ContainsKey("print"))
        {
            var printData = doc["print"];
            foreach (var kvp in printData)
            {
                Data[kvp.Key] = kvp.Value?.ToString();

                if (!SubscriptionKeyMap.TryGetValue(kvp.Key, out var func))
                {
                    continue;
                }

                await func?.Invoke(kvp.Key, Data[kvp.Key]);
            }

            if (null != OnDataUpdatedAsync)
            {
                await OnDataUpdatedAsync.Invoke(Data);
            }
        }
    }
}
