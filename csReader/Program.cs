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
            UInt32 e1 = SerialLib.SerialPortOpen(sp);
            UInt32 e3 = SerialLib.SerialPortConfig(sp, 9600, 10);

            Console.WriteLine("csReader: ready to read, press ENTER to continue");
            Console.ReadLine();

            StringBuilder sb = new StringBuilder(1024);
            uint sbSize;
            UInt32 e4 = SerialLib.SerialPortRead(sp, sb, out sbSize);

            UInt32 e2 = SerialLib.SerialPortClose(sp);
            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
