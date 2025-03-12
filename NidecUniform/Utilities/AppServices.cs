using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.ViewModels;
using NidecUniform.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Utilities
{

    public static class AppServices
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Đăng ký DbContext
            //services.AddDbContext<NidecUniformContext>(options =>
            //    options.UseSqlServer("Data Source=LAPTOP-99421S3D\\SQLEXPRESS; Initial Catalog=NIDEC_UNIFORM; Integrated Security=True; Encrypt=True; Trust Server Certificate=True"));
            services.AddDbContext<NidecUniformContext>(options =>
               options.UseSqlServer("Data Source=10.234.1.89;Initial Catalog=NidecUniform;Persist Security Info=True;User ID=sa;Password=sa;Encrypt=True;Trust Server Certificate=True"));
            // Đăng ký Repository
            services.AddScoped<IEmpoloyee, EmployeeRepository>();
            services.AddScoped<IRawData, RawDataRepository>();
            services.AddScoped<IRequest, RequestRepository>();
            services.AddScoped<IRequestDetails, RequestDetailsRepository>();
            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<IDelivery, DeliveryRepository>();
            services.AddScoped<IDeliveryDetail, DeliveryDetailRepository>();

            // Trong App.xaml.cs hoặc nơi bạn đăng ký dịch vụ DI
            services.AddTransient<Import>(); // Đăng ký Import UserControl
            services.AddTransient<IUIServices>(sp => sp.GetRequiredService<Import>());
            services.AddTransient<ImportVM>();
            // Đăng ký ViewModel (không cần inject constructor)
            services.AddSingleton<HomeVM>();
            services.AddSingleton<ExportVM>();
            services.AddSingleton<ImportVM>();
            services.AddSingleton<ScanVM>();
            services.AddSingleton<NavigationVM>();

            // Đăng ký MainWindow
            services.AddTransient<MainWindow>();

            // Build ServiceProvider
            ServiceProvider = services.BuildServiceProvider();
        }

        // Hàm lấy Service khi cần
        public static T GetService<T>() where T : class
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
