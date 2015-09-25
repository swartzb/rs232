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
            //UInt32 err;

            //SWL.SerialPort sp4 = new SWL.SerialPort("COM4");
            //err = sp4.Open();
            //err = sp4.Config();
            //err = sp4.Flush();

            //SWL.SerialPort sp5 = new SWL.SerialPort("COM5");
            //err = sp5.Open();
            //err = sp5.Config();
            //err = sp5.Flush();

            //err = sp4.Write("hello\r");
            //StringBuilder sb = new StringBuilder(1024);
            //UInt32 sbSize;
            //err = sp5.Read(sb, out sbSize);

            //err = sp4.Close();
            //err = sp5.Close();

            InitializeComponent();

            //this.Content = sb.ToString();
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
        DateTime FinishTime;

        bool AreWeDone()
        {
            TimeSpan remaining = FinishTime - DateTime.Now;
            timeSpan.TimeRemaining = new TimeSpan(remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);
            if (remaining > TimeSpan.FromSeconds(0.0))
            {
                return false;
            }
            else
            {
                lbPorts.IsEnabled = true;
                btnStart.IsEnabled = true;
                btnCancel.IsEnabled = false;
                timeSpan.AreButtonsEnabled = true;
                return true;
            }
        }

        void OnTxMessage(string msg)
        {
            TxMessage = msg;
        }

        void OnRxMessage(string msg)
        {
            RxMessage = msg;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            btnStart.IsEnabled = (lb.SelectedItems.Count == 2);
            return;
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            RxReady = new AutoResetEvent(false);

            RxMessage = string.Empty;
            TxMessage = string.Empty;
            lbPorts.IsEnabled = false;
            btnStart.IsEnabled = false;
            btnCancel.IsEnabled = true;
            timeSpan.AreButtonsEnabled = false;
            FinishTime = DateTime.Now + timeSpan.TimeRemaining;

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(RxThreadProc), lbPorts.SelectedItems[1]);
            RxReady.WaitOne();
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(TxThreadProc), lbPorts.SelectedItems[0]);
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
