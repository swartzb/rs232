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

        [DllImport("SerialLib.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 SerialPortOpen(IntPtr sp);

        [DllImport("SerialLib.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 SerialPortClose(IntPtr sp);

        [DllImport("SerialLib.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 SerialPortConfig(IntPtr sp, UInt32 dwBaudRate, UInt32 dwTimeOutInSec);

        [DllImport("SerialLib.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 SerialPortWrite(IntPtr sp, [MarshalAs(UnmanagedType.LPStr)] string buf, UInt32 bufSize);

        [DllImport("SerialLib.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 SerialPortRead(IntPtr sp, StringBuilder buf, out UInt32 bufSize);

        [DllImport("SerialLib.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern UInt32 SerialPortFlush(IntPtr sp);
    }
}
