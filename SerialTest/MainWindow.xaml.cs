using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace SerialTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void OnTimerDone(object sender, EventArgs e)
        {
            RxTxComplete.Set();
            return;
        }

        public LogFile TxLog
        {
            get
            {
                if (_TxLog == null)
                {
                    _TxLog = new LogFile("Tx");
                }
                return _TxLog; 
            }
        }
        private LogFile _TxLog = null;

        public LogFile RxLog
        {
            get
            {
                if (_RxLog == null)
                {
                    _RxLog = new LogFile("Rx");
                }
                return _RxLog;
            }
        }
        private LogFile _RxLog = null;

        public string[] PortNames
        {
            get
            {
                if (_PortNames == null)
                {
                    _PortNames = System.IO.Ports.SerialPort.GetPortNames();
                }
                return _PortNames; 
            }
        }
        private string[] _PortNames = null;

        public string TxMessage
        {
            get { return _TxMessage; }
            set
            {
                if(_TxMessage != value)
                {
                    TxLog.Append(value);
                    _TxMessage = value;
                    RaisePropertyChanged("TxMessage");
                }
            }
        }
        private string _TxMessage = string.Empty;

        public string RxMessage
        {
            get { return _RxMessage; }
            set
            {
                if (_RxMessage != value)
                {
                    RxLog.Append(value);
                    _RxMessage = value;
                    RaisePropertyChanged("RxMessage");
                }
            }
        }
        private string _RxMessage = string.Empty;

        public int ThreadCount
        {
            get { return _ThreadCount; }
            set
            {
                if (_ThreadCount != value)
                {
                    _ThreadCount = value;
                    RaisePropertyChanged("ThreadCount");
                }
            }
        }
        private int _ThreadCount = 0;

        void DecrementThreadCount()
        {
            --ThreadCount;
        }

        AutoResetEvent ServerReady;
        ManualResetEvent RxTxComplete;

        void OnTxMessage(string msg)
        {
            TxMessage = msg;
        }

        void OnRxMessage(string msg)
        {
            RxMessage = msg;
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            ServerReady = new AutoResetEvent(false);
            RxTxComplete = new ManualResetEvent(false);
            ThreadCount = 2;

            RxMessage = string.Empty;
            TxMessage = string.Empty;

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(ServerThreadProc), lbPorts.SelectedItems[1]);
            ServerReady.WaitOne();
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(ClientThreadProc), lbPorts.SelectedItems[0]);
            timeRemaining.IsRunning = true;
        }

#region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

#endregion

        private void OnCancel(object sender, RoutedEventArgs e)
        {

        }
    }
}
