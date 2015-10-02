using System;
using System.Text;
using System.Threading;
using SWL = SerialWrapperLib;

namespace SerialTest
{
    public partial class MainWindow
    {
        public void ClientThreadProc(object state)
        {
            log.Info("ClientThreadProc enter");
            
            string clientPort = (string)state;
            Action<string> onNewTxMessage = new Action<string>(OnTxMessage);
            Action<string> onNewRxMessage = new Action<string>(OnRxMessage);
            Action amDone = new Action(DecrementThreadCount);

            SWL.SerialPort spClient = new SWL.SerialPort(clientPort);
            uint err = spClient.Open();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Client Open ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            err = spClient.Config();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Client Config ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            err = spClient.Flush();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Client Flush ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            StringBuilder sb = new StringBuilder(1024);
            UInt32 sbSize, eventMask;

            while (!RxTxComplete.WaitOne(0))
            {
                DateTime dtNow = DateTime.Now;
                string msg = dtNow.ToString("MMMM dd, yyyy HH:mm:ss.f");
                err = spClient.Write(msg);
                if (err != 0)
                {
                    Dispatcher.Invoke(onNewTxMessage, "Client Write ERROR: " + err.ToString());
                    break;
                }
                else
                {
                    Dispatcher.Invoke(onNewTxMessage, msg);
                }

                err = spClient.Read(sb, out sbSize, out eventMask);
                if (err != 0)
                {
                    Dispatcher.Invoke(onNewRxMessage, "Client Read ERROR: " + err.ToString());
                    break;
                }
                else
                {
                    Dispatcher.Invoke(onNewRxMessage, sb.ToString());
                }

                Thread.Sleep(1000);
            }

            err = spClient.Close();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Client Close ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            Dispatcher.Invoke(amDone);

            log.Info("ClientThreadProc exit");
        }

        public void ServerThreadProc(object state)
        {
            log.Info("ServerThreadProc enter");

            string clientPort = (string)state;
            Action<string> onNewTxMessage = new Action<string>(OnTxMessage);
            Action<string> onNewRxMessage = new Action<string>(OnTxMessage);
            Action amDone = new Action(DecrementThreadCount);

            SWL.SerialPort spClient = new SWL.SerialPort(clientPort);
            uint err = spClient.Open();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Server Open ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            err = spClient.Config();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Server Config ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            err = spClient.Flush();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Server Flush ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            StringBuilder sb = new StringBuilder(1024);
            UInt32 sbSize, eventMask;
            ServerReady.Set();

            while (!RxTxComplete.WaitOne(0))
            {
                string msg = string.Empty;

                err = spClient.Read(sb, out sbSize, out eventMask);
                if (err != 0)
                {
                    Dispatcher.Invoke(onNewRxMessage, "Server Read ERROR: " + err.ToString());
                    break;
                }
                else
                {
                    msg = sb.ToString();
                }

                err = spClient.Write(msg);
                if (err != 0)
                {
                    Dispatcher.Invoke(onNewTxMessage, "Server Write ERROR: " + err.ToString());
                    break;
                }
            }

            err = spClient.Close();
            if (err != 0)
            {
                Dispatcher.Invoke(onNewTxMessage, "Server Close ERROR: " + err.ToString());
                Dispatcher.Invoke(amDone);
                return;
            }

            Dispatcher.Invoke(amDone);

            log.Info("ServerThreadProc exit");
        }
    }
}
