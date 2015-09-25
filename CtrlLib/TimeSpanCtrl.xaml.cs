using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CtrlLib
{
    /// <summary>
    /// Interaction logic for TimeSpanCtrl.xaml
    /// </summary>
    public partial class TimeSpanCtrl : UserControl, INotifyPropertyChanged
    {
        public TimeSpanCtrl()
        {
            InitializeComponent();
        }

        private void OnIncreaseHoursClick(object sender, RoutedEventArgs e)
        {
            TimeRemaining += TimeSpan.FromHours(1.0);
        }

        private void OnDecreaseHoursClick(object sender, RoutedEventArgs e)
        {
            TimeRemaining -= TimeSpan.FromHours(1.0);
        }

        private void OnIncreaseMinutesClick(object sender, RoutedEventArgs e)
        {
            TimeRemaining += TimeSpan.FromMinutes(1.0);
        }

        private void OnDecreaseMinutesClick(object sender, RoutedEventArgs e)
        {
            TimeRemaining -= TimeSpan.FromMinutes(1.0);
        }

        private void OnIncreaseSecondsClick(object sender, RoutedEventArgs e)
        {
            TimeRemaining += TimeSpan.FromSeconds(1.0);
        }

        private void OnDecreaseSecondsClick(object sender, RoutedEventArgs e)
        {
            TimeRemaining -= TimeSpan.FromSeconds(1.0);
        }

        public bool AreButtonsEnabled
        {
            get { return _AreButtonsEnabled; }
            set
            {
                if (_AreButtonsEnabled != value)
                {
                    _AreButtonsEnabled = value;
                    RaisePropertyChanged("AreButtonsEnabled");
                }
            }
        }
        public bool _AreButtonsEnabled = true;

        public TimeSpan TimeRemaining
        {
            get { return _TimeRemaining; }
            set
            {
                if (_TimeRemaining != value)
                {
                    _TimeRemaining = value;
                    RaisePropertyChanged("TimeRemaining");
                }
            }
        }
        public TimeSpan _TimeRemaining = TimeSpan.FromSeconds(0.0);


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
