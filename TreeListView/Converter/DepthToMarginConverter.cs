using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TreeListView.Converter
{
    internal class DepthToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeListViewItem item)
                return new Thickness((item.Depth * 12) + 19, 0, 0, 0);

            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
