using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Utilities;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NidecUniform
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Khởi tạo Dependency Injection
            AppServices.ConfigureServices();

            // Lấy MainWindow từ DI
            var mainWindow = AppServices.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
       


