using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NidecUniform.Models;
using NidecUniform.Repositories;
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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký DbContext
            services.AddDbContext<NidecUniformContext>(options =>
                options.UseSqlServer("Data Source = LAPTOP - 99421S3D\\SQLEXPRESS; Initial Catalog = NIDEC_UNIFORM; Integrated Security = True; Encrypt=True;Trust Server Certificate=True"));

            // Đăng ký Repository
            services.AddScoped<IEmpoloyeeRepository, EmployeeRepository>();

            // Đăng ký MainWindow với tham số từ DI
            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>(); // Lấy service từ DI
            mainWindow.Show();
        }

    }

}
