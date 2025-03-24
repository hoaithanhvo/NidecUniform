using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using NidecUniform.ViewModels;
using NidecUniform.Views;
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

            AppServices.ConfigureServices();

            var mainWindow = AppServices.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
       


