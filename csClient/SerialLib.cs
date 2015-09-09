using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace csClient
{
    public class SerialLib
    {
        [DllImport("SerialLib.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SerialPortInit([MarshalAs(UnmanagedType.LPWStr)] string portName);

        [DllImport("SerialLib.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void SerialPortDestroy(IntPtr sp);
    }
}
