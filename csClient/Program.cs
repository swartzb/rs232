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
            Int32 h1 = SerialLib.SerialPortOpen(sp);
            Int32 h2 = SerialLib.SerialPortClose(sp);
            SerialLib.SerialPortDestroy(sp);
            return;
        }
    }
}
