using System;
using System.Net.Sockets;

public class Program {
    static TcpClient client;


    static void Main()
    {
        client = new TcpClient();
        byte[] IP = { 192, 168, 0, 100 };
        System.Net.IPAddress iPAddress = new System.Net.IPAddress(IP);
        client.Connect(iPAddress, 8888);

        byte[] writeData = { 0x02 };
        byte[] readData;

        for(int i = 0; i < 1000; i++)
        {
            i2cWriteRead(0x6A, writeData, 3, out readData);

            for(int j =0; j<readData.Length; j++)
            {
                string output = $"{readData[0]} {readData[1]} {readData[2]}";

                Console.WriteLine(output);
            }
        }
        


    }

    static void i2cWriteRead( ushort chipAddr, in byte[] writeData, ushort readLen, out byte[] readData  )
    {
        readData = null;

        Msg msg = new Msg();

        msg.hdr.signature = 0x1234;

        msg.payload.i2c.tran.v1.req.chipAddr = chipAddr;
        msg.payload.i2c.tran.v1.req.wrLen = (ushort) writeData.Length;


        for(int i = 0; i < writeData.Length; i++)
        {
            msgMod.uint8_t_arr_setitem(msg.payload.i2c.tran.v1.req.wrBuff, i, writeData[i]);
        }
        
        msg.payload.i2c.tran.v1.req.rdLen = readLen;

        int msgSize = msgMod.getMsgSize();

        byte[] data = new byte[msgSize];

        SWIGTYPE_p_unsigned_char pData = msgMod.typeCastMsgToUint8(msg);

        for (int i = 0; i < msgSize; i++)
        {
            data[i] = msgMod.uint8_t_arr_getitem(pData, i);
        }

        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);

        data = new byte[msgSize];

        int readlength = stream.Read(data, 0, data.Length);

        if(readlength != data.Length)
        {
            Console.WriteLine("Read Error");
        }

        for(int i = 0; i < msgSize; i++)
        {
            msgMod.uint8_t_arr_setitem(pData, i, data[i]);
        }

        readData = new byte[readLen];
        for(int i = 0; i < readLen; i++)
        {
            readData[i] = msgMod.uint8_t_arr_getitem(msg.payload.i2c.tran.v1.resp.rdBuff, i);
        }

    }
}
