using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SWL = SerialWrapperLib;

namespace SerialTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public App()
        {

            Style sty = (Style)TryFindResource(typeof(System.Windows.Controls.Button));
            sty = (Style)TryFindResource(typeof(System.Windows.Controls.Border));
            sty = (Style)TryFindResource(typeof(System.Windows.Controls.Label));
            sty = (Style)TryFindResource(typeof(System.Windows.Controls.ListBox));
            sty = (Style)TryFindResource(typeof(System.Windows.Controls.ListBoxItem));

            return;
        }

        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            log.Info("App Startup");
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            log.Info("App Exit");
        }
    }
}
