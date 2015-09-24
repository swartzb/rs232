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
            TheTimeSpan += TimeSpan.FromHours(1.0);
        }

        private void OnDecreaseHoursClick(object sender, RoutedEventArgs e)
        {
            TheTimeSpan -= TimeSpan.FromHours(1.0);
        }

        private void OnIncreaseMinutesClick(object sender, RoutedEventArgs e)
        {
            TheTimeSpan += TimeSpan.FromMinutes(1.0);
        }

        private void OnDecreaseMinutesClick(object sender, RoutedEventArgs e)
        {
            TheTimeSpan -= TimeSpan.FromMinutes(1.0);
        }

        private void OnIncreaseSecondsClick(object sender, RoutedEventArgs e)
        {
            TheTimeSpan += TimeSpan.FromSeconds(1.0);
        }

        private void OnDecreaseSecondsClick(object sender, RoutedEventArgs e)
        {
            TheTimeSpan -= TimeSpan.FromSeconds(1.0);
        }

        public TimeSpan TheTimeSpan
        {
            get { return _TheTimeSpan; }
            set
            {
                if (_TheTimeSpan != value)
                {
                    _TheTimeSpan = value;
                    RaisePropertyChanged("TheTimeSpan");
                }
            }
        }
        public TimeSpan _TheTimeSpan = TimeSpan.FromSeconds(0.0);


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
