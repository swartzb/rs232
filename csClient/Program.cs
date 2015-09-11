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
            UInt32 e = SerialLib.SerialPortOpen(sp);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortOpen: {0}", e);
                return;
            }

            e = SerialLib.SerialPortConfig(sp, 9600, 5);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortConfig: {0}", e);
                return;
            }

            Console.WriteLine("csClient: ready to write, press ENTER to continue");
            Console.ReadLine();
            
            string buf = "hello\r";
            e = SerialLib.SerialPortWrite(sp, buf, (uint)buf.Length);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortWrite: {0}", e);
                return;
            }

            e = SerialLib.SerialPortClose(sp);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortClose: {0}", e);
                return;
            }

            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
