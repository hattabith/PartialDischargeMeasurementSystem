using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Management;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Text;


internal class Program
{
    private static async Task Main(string[] args)
    {
        UsbDeviceFinder finder = new UsbDeviceFinder(0x5345, 0x1234); // Замініть VID та PID на ваші значення USB\VID_5345&PID_1234\2148011
        UsbRegDeviceList usbRegistry = UsbDevice.AllDevices;

        // Показати перелік доступних пристроїв
        // USB\VID_5345&PID_1234\2148011


        // IP-адреса та порт осцилографа
        const string ipAddress = "192.168.88.72";
        const int port = 80;

        // Команда SCPI для отримання хвильової форми з каналу 1
        // string scpiCommand = ":DATA:WAVE:SCREen:CH2?";

        // byte[] waveformData = SendSCPICommand(ipAddress, port, scpiCommand);

        // Обробити отримані бінарні дані хвильової форми
        // Тут вам потрібно реалізувати логіку обробки бінарних даних

        // Приклад: вивести перші 10 байт
        var oReader = new OscilloscopeReader(ipAddress, port);
        try
        {
            var data = await oReader.ReadDataAsync();
            oReader.DisplayData(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при отриманні даних: {ex.Message}");
        }
    }
}