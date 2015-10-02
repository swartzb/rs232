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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        static extern UInt32 ReadSerialPort(IntPtr h, StringBuilder buf, UInt32 bufSize, out UInt32 bufLen, out UInt32 eventMask);

        public SerialPort(string portName)
        {
            PortName = "\\\\.\\" + portName;
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
            string outMsg = msg + "\r";
            UInt32 rVal = WriteSerialPort(Handle, outMsg, (uint)outMsg.Length);
            log.DebugFormat("Write {0} '{1}'", rVal, msg);
            return rVal;
        }

        public UInt32 Read(StringBuilder sb, out UInt32 bufLen, out UInt32 eventMask)
        {
            UInt32 rVal = ReadSerialPort(Handle, sb, (uint)sb.Capacity, out bufLen, out eventMask);
            log.DebugFormat("Read {0} {1} {2:X8} '{3}'", rVal, bufLen, eventMask, sb.ToString());
            return rVal;
        }
    }
}
