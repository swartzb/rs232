using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csReader
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr sp = SerialLib.SerialPortInit("\\\\.\\COM5");
            UInt32 e = SerialLib.SerialPortOpen(sp);
            if (e != 0)
            {
                Console.WriteLine("csReader SerialPortOpen: {0}", e);
                return;
            }

            e = SerialLib.SerialPortConfig(sp, 9600, 10);
            if (e != 0)
            {
                Console.WriteLine("csReader SerialPortConfig: {0}", e);
                return;
            }

            Console.WriteLine("csReader: ready to read, press ENTER to continue");
            Console.ReadLine();

            StringBuilder sb = new StringBuilder(1024);
            uint sbSize;
            e = SerialLib.SerialPortRead(sp, sb, out sbSize);
            if (e != 0)
            {
                Console.WriteLine("csReader SerialPortRead: {0}", e);
                return;
            }
            else
            {
                Console.WriteLine("csReader SerialPortRead success: {0}", sb.ToString());
            }

            e = SerialLib.SerialPortClose(sp);
            if (e != 0)
            {
                Console.WriteLine("csReader SerialPortClose: {0}", e);
                return;
            }

            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
