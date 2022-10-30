using System.Globalization;

namespace StagingApp.Controls.Library.Converters;
public class BoolToVisConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            if (parameter is string str && str == "Inverse")
            {
                return b ? Visibility.Visible : Visibility.Hidden;
            }
            return b ? Visibility.Hidden : Visibility.Visible;
        }
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
