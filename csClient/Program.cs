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
            IntPtr sp = SerialLib.SerialPortInit("COM4");
            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
