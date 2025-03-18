using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NidecUniform.Utilities
{
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not RequestDetail item)
                return new SolidColorBrush(Colors.White);

            if (!checkValidDate(item))
                return new SolidColorBrush(Colors.Gray);
            if (item.QuantityDelivered == item.QuantityRequested)
                return new SolidColorBrush(Colors.LightGreen);

            if (item.QuantityDelivered == 0)
                return new SolidColorBrush(Colors.White);

            return item.QuantityDelivered < item.QuantityRequested
                ? new SolidColorBrush(Colors.Orange)
                : new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool checkValidDate(RequestDetail item)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            return today >= item.StartDate && today <= item.EndDate;
        }
    }
}
