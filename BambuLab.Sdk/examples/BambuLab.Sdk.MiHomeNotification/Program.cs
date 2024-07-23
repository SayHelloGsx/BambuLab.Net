using BambuLab.Sdk.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiHome.Net.Dto;
using MiHome.Net.Middleware;
using MiHome.Net.Service;

namespace BambuLab.Sdk.MiHomeNotification
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = await RegistrateServiceAsync();
            var miHomeDriver = host.Services.GetRequiredService<IMiHomeDriver>();

            await Console.Out.WriteLineAsync("Listening... Press any key to exit...");
            await using (var printer = await ConnectToPrinterAsync(host.Services.GetRequiredService<IConfiguration>()))
            {
                printer.OnPrintStatusChanged += async (status, gcodeState) =>
                {
                    await Console.Out.WriteLineAsync($"Status changed. PrintStatus: {status}, GcodeState: {gcodeState}");

                    var needNotification = false;

                    switch (status)
                    {
                        case PrintStatusEnum.M400_PAUSE:
                        case PrintStatusEnum.PAUSED_FILAMENT_RUNOUT:
                        case PrintStatusEnum.PAUSED_USER:
                        case PrintStatusEnum.PAUSED_FRONT_COVER_FALLING:
                        case PrintStatusEnum.PAUSED_NOZZLE_TEMPERATURE_MALFUNCTION:
                        case PrintStatusEnum.PAUSED_HEAT_BED_TEMPERATURE_MALFUNCTION:
                        case PrintStatusEnum.PAUSED_SKIPPED_STEP:
                        case PrintStatusEnum.PAUSED_AMS_LOST:
                        case PrintStatusEnum.PAUSED_LOW_FAN_SPEED_HEAT_BREAK:
                        case PrintStatusEnum.PAUSED_CHAMBER_TEMPERATURE_CONTROL_ERROR:
                        case PrintStatusEnum.PAUSED_USER_GCODE:
                        case PrintStatusEnum.PAUSED_NOZZLE_FILAMENT_COVERED_DETECTED:
                        case PrintStatusEnum.PAUSED_CUTTER_ERROR:
                        case PrintStatusEnum.PAUSED_FIRST_LAYER_ERROR:
                        case PrintStatusEnum.PAUSED_NOZZLE_CLOG:
                            needNotification = true;
                            break;
                    }

                    switch (gcodeState)
                    {
                        case GcodeStateEnum.PAUSE:
                            needNotification = true;
                            break;
                    }

                    if (needNotification)
                    {
                        await Console.Out.WriteLineAsync($"The print task has been paused. Time:{DateTime.Now}, PrintStatus: {status}, GcodeState: {gcodeState}");
                        await SendNotificationAsync(miHomeDriver, "打印任务暂停，请检查打印机状态！");
                    }
                };

                while (true)
                {
                    if (!printer.IsMqttConnected)
                    {
                        await printer.ConnectAsync();
                    }

                    if (Console.KeyAvailable)
                    {
                        break;
                    }

                    await Task.Delay(1000);
                }
            }
        }

        static Task<IHost> RegistrateServiceAsync()
        {
            var cfgBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.secrets.json", true, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
            
            var configuration = cfgBuilder.Build();

            var hostBuilder = Host.CreateDefaultBuilder();

            hostBuilder.ConfigureServices(services => services.AddMiHomeDriver(x =>
            {
                x.UserName = configuration["MiHome:Account"];
                x.Password = configuration["MiHome:Password"];
            }));

            hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddConfiguration(configuration);
            });

            var host = hostBuilder.Build();

            return Task.FromResult(host);
        }

        static async Task SendNotificationAsync(IMiHomeDriver miHomeDriver, string message)
        {
            //list all mi home drivers
            var deviceList = await miHomeDriver.Cloud.GetDeviceListAsync();
            var xiaoai = deviceList.First(it => it.Name.Equals("小爱音箱Pro"));

            await miHomeDriver.Cloud.CallActionAsync(new CallActionInputDto()
            {
                Did = xiaoai.Did,
                Aiid = 1,
                Siid = 5,
                In = new List<string>() { message }
            });
        }

        static async Task<IPrinter> ConnectToPrinterAsync(IConfiguration configuration)
        {
            var chinaCloud = new ChinaBambuLabCloudRequester();
            await chinaCloud.LoginAsync(configuration["BambuLab:Account"], configuration["BambuLab:Password"]);
            var device = await chinaCloud.GetDeviceList();

            var SERIAL = device.GetDevId(0);
            var ACCESS_CODE = chinaCloud.AccessToken;

            var printer = new ChinaCloudPrinter(ACCESS_CODE, SERIAL, chinaCloud.UserName);
            await printer.ConnectAsync();
            return printer;
        }
    }
}