using NidecUniform.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NidecUniform.Utilities
{
    public class LoadingService
    {
        private static LoadingWindow _loadingWindow;
        private static bool _isShowing = false;

        public static void Show()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_isShowing) return;
                _loadingWindow = new LoadingWindow();
                _loadingWindow.Owner = Application.Current.MainWindow; // Đặt Owner
                _loadingWindow.Show();
                _isShowing = true;
            });
        }

        public static void Close()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_loadingWindow != null)
                {
                    _loadingWindow.Close();
                    _loadingWindow = null;
                    _isShowing = false;
                }
            });
        }
    }
}
