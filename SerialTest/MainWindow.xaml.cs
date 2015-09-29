using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SWL = SerialWrapperLib;

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

        AutoResetEvent RxReady;

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
            RxReady = new AutoResetEvent(false);

            RxMessage = string.Empty;
            TxMessage = string.Empty;

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(RxThreadProc), lbPorts.SelectedItems[1]);
            RxReady.WaitOne();
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(TxThreadProc), lbPorts.SelectedItems[0]);
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
