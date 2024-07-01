using BambuLab.Sdk;
using BambuLab.Sdk.Http;

namespace BambuLab.Sdk.ExampleConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var chinaCloud = new ChinaBambuLabCloudRequester("***", "***");
            await chinaCloud.LoginAsync();
            var device = await chinaCloud.GetDeviceList();

            var SERIAL = device.GetDevId(0);
            var ACCESS_CODE = chinaCloud.AccessToken;

            var printer = new ChinaCloudPrinter(ACCESS_CODE, SERIAL, chinaCloud.UserName);
            await printer.ConnectAsync();
            await Task.Delay(5000);
            await Console.Out.WriteLineAsync((await printer.GetPrinterStateAsync()).ToString());
            await Console.Out.WriteLineAsync((await printer.GetCurrentStateAsync()).ToString());

            await Console.Out.WriteLineAsync((await printer.GetBedTemperature()).ToString());
            await Console.Out.WriteLineAsync((await printer.GetNozzleTemperatureAsync()).ToString());

            while (true)
            {
                var key = Console.ReadKey();
                if (key.KeyChar == 'w')
                {
                    await printer.TurnLightOffAsync();
                    //await printer.HomePrinterAsync();
                }

                if (key.KeyChar == 's')
                {
                    await printer.TurnLightOnAsync();
                    //await printer.HomePrinterAsync();
                }

                if (key.KeyChar == 'h')
                {
                    await printer.HomePrinterAsync();
                }

                if (key.KeyChar == '1')
                {
                    await printer.SetPrintSpeedAsync(PrintSpeedEnum.Sport);
                }

                if (key.KeyChar == '2')
                {
                    //await printer.UnloadFilamentSpoolAsync();
                }

                if (key.KeyChar == 'z')
                {
                    break;
                }
            }

            await printer.DisconnectAsync();
        }
    }
}
