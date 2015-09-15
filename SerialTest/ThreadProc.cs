using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWL = SerialWrapperLib;

namespace SerialTest
{
    public partial class MainWindow
    {
        public void ThreadProc(object state)
        {
            string rxPort = (string)state;

            SWL.SerialPort spRx = new SWL.SerialPort(rxPort);
            uint err = spRx.Open();
            if (err != 0)
            {
                return;
            }

            err = spRx.Config();
            if (err != 0)
            {
                return;
            }

            err = spRx.Flush();
            if (err != 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder(1024);
            UInt32 sbSize;
            RxReady.Set();
            err = spRx.Read(sb, out sbSize);
            if (err != 0)
            {
                return;
            }
            else
            {
                Action<string> onNewMessage = new Action<string>(OnRxMessage);
                Dispatcher.Invoke(onNewMessage, sb.ToString());
            }

            err = spRx.Close();
            if (err != 0)
            {
                return;
            }
        }
    }
}
