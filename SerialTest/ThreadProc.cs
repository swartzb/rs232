using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SWL = SerialWrapperLib;

namespace SerialTest
{
    public partial class MainWindow
    {
        int MAX_I = 5;
        
        public void TxThreadProc(object state)
        {
            string txPort = (string)state;
            Action<string> onNewMessage = new Action<string>(OnTxMessage);

            SWL.SerialPort spTx = new SWL.SerialPort(txPort);
            uint err = spTx.Open();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }

            err = spTx.Config();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }

            err = spTx.Flush();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }

            for (int i = 0; i < MAX_I; i++)
            {
                DateTime dtNow = DateTime.Now;
                string msg = dtNow.ToString("yyyy.MM.dd.HH.mm.ss.ff");
                err = spTx.Write(msg + "\r");
                if (err != 0)
                {
                    Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                    break;
                }
                else
                {
                    Dispatcher.Invoke(onNewMessage, msg);
                }

                Thread.Sleep(TimeSpan.FromSeconds(1.0));
            }

            err = spTx.Close();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }
        }

        public void RxThreadProc(object state)
        {
            string rxPort = (string)state;
            Action<string> onNewMessage = new Action<string>(OnRxMessage);

            SWL.SerialPort spRx = new SWL.SerialPort(rxPort);
            uint err = spRx.Open();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }

            err = spRx.Config();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }

            err = spRx.Flush();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }

            StringBuilder sb = new StringBuilder(1024);
            UInt32 sbSize;
            RxReady.Set();

            for (int i = 0; i < MAX_I; i++)
            {
                err = spRx.Read(sb, out sbSize);
                if (err != 0)
                {
                    Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                    break;
                }
                else
                {
                    Dispatcher.Invoke(onNewMessage, sb.ToString());
                }
                
            }
            err = spRx.Close();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewMessage, "ERROR: " + err.ToString());
                return;
            }
        }
    }
}
