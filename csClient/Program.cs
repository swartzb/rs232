using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //CppLib.CSyncSerialComm sp1 = new CppLib.CSyncSerialComm("COM4");

            IntPtr sp = SerialLib.SerialPortInit("\\\\.\\COM4");
            UInt32 e1 = SerialLib.SerialPortOpen(sp);
            UInt32 e3 = SerialLib.SerialPortConfig(sp, 9600, 5);

            Console.WriteLine("csClient: ready to write, press ENTER to continue");
            Console.ReadLine();
            
            string buf = "hello\r";
            UInt32 e4 = SerialLib.SerialPortWrite(sp, buf, (uint)buf.Length);

            UInt32 e2 = SerialLib.SerialPortClose(sp);
            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
