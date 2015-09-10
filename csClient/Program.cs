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
            CppLib.CSyncSerialComm sp1 = new CppLib.CSyncSerialComm("COM4");
            
            IntPtr sp = SerialLib.SerialPortInit("COM4");
            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
