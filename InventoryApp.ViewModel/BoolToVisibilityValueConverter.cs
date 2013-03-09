using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace InventoryApp.ViewModel
{
    public class BoolToVisibilityValueConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object convertedValue = null;

            if (targetType == typeof(System.Windows.Visibility))
            {
                bool sourceBool = (bool)value;
                convertedValue = sourceBool ? Visibility.Collapsed : Visibility.Visible;
            }

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
