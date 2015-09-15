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
                    _RxMessage = value;
                    RaisePropertyChanged("RxMessage");
                }
            }
        }
        private string _RxMessage = string.Empty;

        AutoResetEvent RxReady;

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

        private void OnClick(object sender, RoutedEventArgs e)
        {
            RxReady = new AutoResetEvent(false);

            SWL.SerialPort spTx = new SWL.SerialPort((string)lbPorts.SelectedItems[0]);
            uint err = spTx.Open();
            if (err != 0)
            {
                return;
            }

            err = spTx.Config();
            if (err != 0)
            {
                return;
            }

            err = spTx.Flush();
            if (err != 0)
            {
                return;
            }

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(ThreadProc), lbPorts.SelectedItems[1]);
            RxReady.WaitOne();
            Thread.Sleep(TimeSpan.FromSeconds(1.0));

            TxMessage = "hello";
            err = spTx.Write(TxMessage + "\r");
            if (err != 0)
            {
                return;
            }

            err = spTx.Close();
            if (err != 0)
            {
                return;
            }
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

    }
}
