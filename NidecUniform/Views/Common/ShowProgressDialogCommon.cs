using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NidecUniform.Views.Common
{
    public static class ShowProgressDialogCommon
    {
        public static void ShowProgressDialog(UserControl userControl)
        {
            if (userControl == null) return;

            // Tạo lớp phủ (overlay)
            Grid overlay = new Grid
            {
                Background = new SolidColorBrush(Colors.Black) { Opacity = 0.5 },
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            // Thêm ProgressBar vào overlay
            ProgressBar progressBar = new ProgressBar
            {
                Width = 200,
                Height = 10,
                IsIndeterminate = true,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            overlay.Children.Add(progressBar);

            // Kiểm tra xem userControl có phải là Grid không
            if (userControl.Content is Grid grid)
            {
                // Kiểm tra RowDefinitions và ColumnDefinitions không phải null trước khi sử dụng
                if (grid.RowDefinitions != null && grid.ColumnDefinitions != null)
                {
                    // Kiểm tra số lượng RowDefinitions và ColumnDefinitions lớn hơn 0 trước khi áp dụng
                    int rowSpan = grid.RowDefinitions.Count > 0 ? grid.RowDefinitions.Count : 1;
                    int columnSpan = grid.ColumnDefinitions.Count > 0 ? grid.ColumnDefinitions.Count : 1;

                    // Đảm bảo overlay phủ toàn bộ nội dung của grid
                    Grid.SetRowSpan(overlay, rowSpan);
                    Grid.SetColumnSpan(overlay, columnSpan);

                    // Thêm overlay vào grid
                    grid.Children.Add(overlay);
                }
                else
                {
                    // Xử lý trường hợp không có RowDefinitions hoặc ColumnDefinitions
                    Console.WriteLine("Grid không có RowDefinitions hoặc ColumnDefinitions");
                }
            }
            else
            {
                // Xử lý nếu userControl.Content không phải là Grid
                Console.WriteLine("userControl.Content không phải là Grid");
            }
        }



        public static void HideProgressDialog(UserControl userControl)
        {
            if (userControl?.Content is Grid grid)
            {
                // Tìm lớp phủ (overlay) có chứa ProgressBar
                var overlay = grid.Children.OfType<Grid>().FirstOrDefault(child => child.Children.OfType<ProgressBar>().Any());
                if (overlay != null)
                {
                    // Xóa overlay khỏi grid
                    grid.Children.Remove(overlay);
                }
            }
        }
    }
}
