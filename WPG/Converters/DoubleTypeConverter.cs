using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace WPG.Converters;

public class DoubleTypeConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value != null)
            return ((double)value).ToString(Thread.CurrentThread.CurrentCulture);

        return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value == null) ? 0.0 : double.Parse((string)value, Thread.CurrentThread.CurrentCulture);
	}
}
