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
        public static DateOnly ToDateOnly(this object value, string format = "dd-MM-yyyy")
        {
            if (value == null) return DateOnly.MinValue; // Ngày mặc định nếu null

            return value switch
            {
                double d => DateOnly.FromDateTime(DateTime.FromOADate(d)), // Nếu là số OADate
                string s when DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                    => DateOnly.FromDateTime(date), // Chuyển từ chuỗi và lấy phần ngày
                DateTime dt => DateOnly.FromDateTime(dt), // Nếu đã là DateTime thì lấy DateOnly
                _ => DateOnly.MinValue // Trả về mặc định nếu lỗi
            };
        }

        public static int ToInt(this object value) => value as int? ?? (int.TryParse(value?.ToString(), out int result) ? result : 0);

        // Chuyển đổi sang bool, mặc định là false
        public static bool ToBool(this object value) => value as bool? ?? (bool.TryParse(value?.ToString(), out bool result) && result);
    }
}
