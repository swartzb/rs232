using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CtrlLib
{
    /// <summary>
    /// Interaction logic for TimeSpanCtrl.xaml
    /// </summary>
    public partial class TimeSpanCtrl : UserControl, INotifyPropertyChanged
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler IsDone;

        public TimeSpanCtrl()
        {
            _CountDownTimer = new DispatcherTimer();
            _CountDownTimer.Interval = TimeSpan.FromSeconds(1.0);
            _CountDownTimer.Tick += OnTick;

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

        public bool IsRunning
        {
            get { return _IsRunning; }
            set
            {
                if (_IsRunning != value)
                {
                    if (value)
                    {
                        _FinishTime = DateTime.Now + TimeRemaining;
                        _CountDownTimer.Start();
                    }
                    else
                    {
                        _CountDownTimer.Stop();

                        if (IsDone != null)
                        {
                            IsDone(this, EventArgs.Empty);
                        }
                    }
                    _IsRunning = value;
                    log.InfoFormat("IsRunning {0}", value);
                    RaisePropertyChanged("IsRunning");
                }
            }
        }
        private bool _IsRunning = false;

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
        private TimeSpan _TimeRemaining = TimeSpan.FromSeconds(0.0);

        DateTime _FinishTime;
        DispatcherTimer _CountDownTimer;

        void OnTick(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            TimeSpan remaining = _FinishTime - dtNow;
            TimeRemaining = new TimeSpan(remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);
            if (TimeRemaining <= TimeSpan.FromSeconds(0.0))
            {
                IsRunning = false;
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
