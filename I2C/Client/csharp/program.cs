using System;
using System.Net.Sockets;

public class Program {

    static void Main()
    {

        TcpClient client = new TcpClient();
        byte[] IP = { 192, 168, 0, 100 };
        System.Net.IPAddress iPAddress = new System.Net.IPAddress(IP);
        client.Connect(iPAddress, 8888);

      

        Msg msg = new Msg();

        msg.hdr.signature = 0x1234;

        msg.payload.i2c.tran.v1.req.chipAddr = 0x6A;
        msg.payload.i2c.tran.v1.req.wrLen = 1;
        msgMod.uint8_t_arr_setitem(msg.payload.i2c.tran.v1.req.wrBuff, 0, 0x02);
        msg.payload.i2c.tran.v1.req.rdLen = 3;

        int msgSize = msgMod.getMsgSize();

        byte[] data = new byte[msgSize];

        SWIGTYPE_p_unsigned_char pData = msgMod.typeCastMsgToUint8(msg);

        for(int i = 0; i < msgSize; i++)
        {
            data[i] = msgMod.uint8_t_arr_getitem(pData, i);
        }



        NetworkStream stream = client.GetStream();
        stream.Write(data, 0, data.Length);


        Console.WriteLine( "Value of foo.x is" + msg.payload.i2c.tran.v1.req.rdLen);

    }
}
