using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Helpers
{
    public static class ExcelHelper
    {
        public static DateTime ToDateTime(this object value, string format = "dd-MM-yyyy")
        {
            if (value == null) return DateTime.MinValue; // Ngày mặc định nếu null

            return value switch
            {
                double d => DateTime.FromOADate(d).Date, // Nếu là số OADate, lấy phần ngày
                string s when DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) => date.Date, // Chuyển từ chuỗi và lấy phần ngày
                _ => DateTime.MinValue // Trả về mặc định nếu lỗi
            };
        }

        // Chuyển đổi sang số nguyên, nếu null trả về 0
        public static int ToInt(this object value) => value as int? ?? (int.TryParse(value?.ToString(), out int result) ? result : 0);

        // Chuyển đổi sang bool, mặc định là false
        public static bool ToBool(this object value) => value as bool? ?? (bool.TryParse(value?.ToString(), out bool result) && result);
    }
}
