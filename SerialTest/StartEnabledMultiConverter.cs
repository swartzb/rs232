using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace SerialTest
{
    public class StartEnabledMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return true;
            }
            
            int threads = (int)values[0];
            int ports = (int)values[1];
            return ((threads == 0) && (ports == 2));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            object[] arr = { 0, 0 };
            return arr;
        }
    }
}
