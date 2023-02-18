
using System;
using System.Net.Sockets;

public class Program {

    static void Main()
    {

        Console.WriteLine("Kannadakkagi Ondannu otti");
        TcpClient client  =  new TcpClient();
        byte[] IP = { 192, 168,0,100 };
        System.Net.IPAddress iPAddress = new System.Net.IPAddress(IP);
        client.Connect(iPAddress, 8888);

        byte[] data = System.Text.Encoding.ASCII.GetBytes("Kannadakkagi ondannu otti\n\r");

        NetworkStream stream = client.GetStream();

        stream.Write(data, 0, data.Length);

    }
}
