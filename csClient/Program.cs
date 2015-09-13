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
            UInt32 e;
            
            IntPtr sp4 = SerialLib.SerialPortInit("\\\\.\\COM4");
            e = SerialLib.SerialPortOpen(sp4);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortOpen COM4: {0}", e);
                return;
            }

            e = SerialLib.SerialPortConfig(sp4, 9600, 5);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortConfig COM4: {0}", e);
                return;
            }

            e = SerialLib.SerialPortFlush(sp4);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortFlush COM4: {0}", e);
                return;
            }

            IntPtr sp5 = SerialLib.SerialPortInit("\\\\.\\COM5");
            e = SerialLib.SerialPortOpen(sp5);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortOpen COM5: {0}", e);
                return;
            }

            e = SerialLib.SerialPortConfig(sp5, 9600, 5);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortConfig COM5: {0}", e);
                return;
            }

            e = SerialLib.SerialPortFlush(sp5);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortFlush COM5: {0}", e);
                return;
            }

            Console.WriteLine("csClient: ready to write, press ENTER to continue");
            Console.ReadLine();
            
            string buf = "hello\r";
            e = SerialLib.SerialPortWrite(sp4, buf, (uint)buf.Length);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortWrite COM4: {0}", e);
                return;
            }

            StringBuilder sb = new StringBuilder(1024);
            uint sbSize;
            e = SerialLib.SerialPortRead(sp5, sb, (uint)sb.Capacity, out sbSize);
            if (e != 0)
            {
                Console.WriteLine("ERROR - csClient SerialPortRead COM5: {0}", e);
                return;
            }
            else
            {
                Console.WriteLine("csClient SerialPortRead COM5: {0}", sb.ToString());
            }
            
            e = SerialLib.SerialPortClose(sp4);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortClose COM4: {0}", e);
                return;
            }

            e = SerialLib.SerialPortClose(sp5);
            if (e != 0)
            {
                Console.WriteLine("csClient SerialPortClose COM5: {0}", e);
                return;
            }

            SerialLib.SerialPortDestroy(sp4);
            return;
        }
    }
}
