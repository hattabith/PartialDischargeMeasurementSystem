using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

public class OscilloscopeReader
{
    private string ipAddress;
    private int port;

    public OscilloscopeReader(string ipAddress, int port)
    {
        this.ipAddress = ipAddress;
        this.port = port;
    }

    public async Task<List<int>> ReadDataAsync()
    {
        using (var client = new TcpClient())
        {
            await client.ConnectAsync(ipAddress, port);
            using (var stream = client.GetStream())
            {
                string command = ":DATA:WAVE:SCREEN:CH2?\n";
                byte[] commandBytes = Encoding.ASCII.GetBytes(command);
                await stream.WriteAsync(commandBytes, 0, commandBytes.Length);

                byte[] responseBuffer = new byte[5000]; // Adjust buffer size as needed
                int bytesRead = await stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);

                return ConvertBytesToInts(responseBuffer, bytesRead);
            }
        }
    }

    private List<int> ConvertBytesToInts(byte[] bytes, int bytesRead)
    {
        List<int> intList = new List<int>();
        for (int i = 0; i < bytesRead; i += 2)
        {
            if (i + 1 < bytesRead)
            {
                int intValue = (bytes[i] << 8) | bytes[i + 1];
                intList.Add(intValue);
            }
        }
        return intList;
    }

    public void DisplayData(List<int> data)
    {
        foreach (var value in data)
        {
            Console.WriteLine(value);
        }
    }
}