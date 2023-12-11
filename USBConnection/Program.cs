using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Management;
using System.Net.Sockets;
using System.Text;


UsbDeviceFinder finder = new UsbDeviceFinder(0x5345, 0x1234); // Замініть VID та PID на ваші значення USB\VID_5345&PID_1234\2148011
UsbRegDeviceList usbRegistry = UsbDevice.AllDevices;

// Показати перелік доступних пристроїв
// USB\VID_5345&PID_1234\2148011


// IP-адреса та порт осцилографа
string ipAddress = "192.168.88.72";
int port = 80;

// Команда SCPI для отримання хвильової форми з каналу 1
string scpiCommand = ":DATA:WAVE:SCREen:CH1?";

// Відправити команду та отримати відповідь
byte[] waveformData = SendSCPICommand(ipAddress, port, scpiCommand);

// Обробити отримані бінарні дані хвильової форми
// Тут вам потрібно реалізувати логіку обробки бінарних даних

// Приклад: вивести перші 10 байт
Console.WriteLine("Байти хвильової форми:");
for (int i = 0; i < waveformData.Count(); i++) 
{
    Console.Write(waveformData[i].ToString("X2") + " ");
}

Console.ReadLine();

System.Half dataFromOscilloscope = (Half)3.14;

static System.Half ConvertToHalf(int twelveBitValue)
{
    // Масштабування 12-бітного числа до 16 біт
    int scaledValue = twelveBitValue << 4;

    // Заповнення недійсних бітів для створення 16-бітного числа
    int fullValue = scaledValue | 0x00008000;

    // Перетворення на System.Half
    float floatValue = BitConverter.ToSingle(BitConverter.GetBytes(fullValue), 0);
    return (System.Half)floatValue;
}
static byte[] SendSCPICommand(string ipAddress, int port, string command)
{
    try
    {
        // Підключення до осцилографа
        using (TcpClient client = new TcpClient(ipAddress, port))
        using (NetworkStream stream = client.GetStream())
        {
            // Відправити команду
            byte[] data = Encoding.ASCII.GetBytes(command + "\n");
            stream.Write(data, 0, data.Length);

            // Отримати відповідь
            byte[] buffer = new byte[1024]; // Зберігати більше даних, ніж очікуєте
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            // Створити масив, який містить лише фактичні отримані дані
            byte[] resultData = new byte[bytesRead];
            Array.Copy(buffer, resultData, bytesRead);

            return resultData;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
        return null;
    }
}
