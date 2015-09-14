using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            UInt32 err;

            SWL.SerialPort sp4 = new SWL.SerialPort("COM4");
            err = sp4.Open();
            err = sp4.Config();
            err = sp4.Flush();

            SWL.SerialPort sp5 = new SWL.SerialPort("COM5");
            err = sp5.Open();
            err = sp5.Config();
            err = sp5.Flush();

            err = sp4.Write("hello\r");
            StringBuilder sb = new StringBuilder(1024);
            UInt32 sbSize;
            err = sp5.Read(sb, out sbSize);

            err = sp4.Close();
            err = sp5.Close();

            InitializeComponent();

            this.Content = sb.ToString();
        }
    }
}
