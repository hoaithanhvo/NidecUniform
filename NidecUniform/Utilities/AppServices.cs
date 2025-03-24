using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Repositories.Interface.Common;
using NidecUniform.ViewModels;
using NidecUniform.Views;
using NidecUniform.Views.Common;
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
            //    options.UseSqlServer("Data Source=LAPTOP-99421S3D\\SQLEXPRESS; Initial Catalog=NidecUniform; Integrated Security=True; Encrypt=True; Trust Server Certificate=True"));
            services.AddDbContextFactory<NidecUniformContext>(options =>
               options.UseSqlServer("Data Source=10.234.1.89;Initial Catalog=NidecUniformV2;Persist Security Info=True;User ID=sa;Password=sa;Encrypt=True;Trust Server Certificate=True"));
            // Đăng ký Repository
            services.AddScoped<IEmployee, EmployeeRepository>();
            services.AddScoped<IRawData, RawDataRepository>();
            services.AddScoped<IRequest, RequestRepository>();
            services.AddScoped<IRequestDetails, RequestDetailsRepository>();
            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<IDelivery, DeliveryRepository>();
            services.AddScoped<IDeliveryDetail, DeliveryDetailRepository>();
            services.AddScoped<ICommon, CommonRepository>();
            services.AddScoped<IExport, ExportRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Trong App.xaml.cs hoặc nơi bạn đăng ký dịch vụ DI
            services.AddTransient<Import>(); // Đăng ký Import UserControl
            //services.AddTransient<IUIServices>(sp => sp.GetRequiredService<Import>());

            // Đăng ký ViewModel (không cần inject constructor)
            services.AddSingleton<HomeVM>();
            services.AddScoped<ExportVM>();
            services.AddSingleton<ImportVM>();
            services.AddSingleton<ScanVM>();
            services.AddSingleton<NavigationVM>();
            services.AddScoped<MainWindow>();

            // Build ServiceProvider
            ServiceProvider = services.BuildServiceProvider();
        }
        public static T GetService<T>() where T : class
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
