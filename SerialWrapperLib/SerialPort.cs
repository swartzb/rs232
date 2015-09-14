using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SerialWrapperLib
{
    public class SerialPort
    {
        [DllImport("SerialLib.dll", EntryPoint = "OpenSerialPort", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern UInt32 OpenSerialPort(out IntPtr h, [InAttribute()] [MarshalAs(UnmanagedType.LPWStr)] string portName);

        [DllImport("SerialLib.dll", EntryPoint = "CloseSerialPort", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern UInt32 CloseSerialPort(IntPtr h);

        [DllImport("SerialLib.dll", EntryPoint = "ConfigSerialPort", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern UInt32 ConfigSerialPort(IntPtr h, UInt32 dwBaudRate, UInt32 dwTimeOutInSec);

        [DllImport("SerialLib.dll", EntryPoint = "FlushSerialPort", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern UInt32 FlushSerialPort(IntPtr h);

        [DllImport("SerialLib.dll", EntryPoint = "WriteSerialPort", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern UInt32 WriteSerialPort(IntPtr h, [InAttribute()] [MarshalAs(UnmanagedType.LPStr)] string buf, UInt32 bufSize);

        [DllImport("SerialLib.dll", EntryPoint = "ReadSerialPort", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern UInt32 ReadSerialPort(IntPtr h, StringBuilder buf, UInt32 bufSize, out UInt32 bufLen);

        public SerialPort(string portName)
        {
            PortName = portName;
        }

        public string PortName { get; set; }

        public IntPtr Handle;

        public UInt32 Open()
        {
            return OpenSerialPort(out Handle, PortName);
        }

        public UInt32 Close()
        {
            return CloseSerialPort(Handle);
        }

        public UInt32 Config()
        {
            return ConfigSerialPort(Handle, 9600, 5);
        }

        public UInt32 Flush()
        {
            return FlushSerialPort(Handle);
        }

        public UInt32 Write(string msg)
        {
            return WriteSerialPort(Handle, msg, (uint)msg.Length);
        }

        public UInt32 Read(StringBuilder sb, out UInt32 bufLen)
        {
            return ReadSerialPort(Handle, sb, (uint)sb.Capacity, out bufLen);
        }
    }
}
