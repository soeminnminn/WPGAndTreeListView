using System;
using System.Globalization;
using System.Windows.Data;

namespace WPG.Converters;

public class EnumTypeConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Enum.GetValues(value.GetType());
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value != null && Enum.TryParse(targetType, value.ToString(), out object val))
            return val;

        return value;
	}
}
