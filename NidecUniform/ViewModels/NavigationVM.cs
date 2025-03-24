using LiveCharts.Wpf;
using NidecUniform.Utilities;
using NidecUniform.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NidecUniform.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ScanCommand { get; set; }


        public NavigationVM()
        {
            HomeCommand = new RelayCommand(_ => CurrentView = AppServices.GetService<HomeVM>());
            ExportCommand = new RelayCommand(async _ =>
            {
                LoadingService.Show();
                var exportVM = AppServices.GetService<ExportVM>();
                CurrentView = exportVM;
                await exportVM.getDataDelivery();
                LoadingService.Close(); // Đóng loading
            });
            ImportCommand = new RelayCommand(_ => CurrentView = AppServices.GetService<ImportVM>());
            ScanCommand = new RelayCommand(_ => CurrentView = AppServices.GetService<ScanVM>());
            CurrentView = AppServices.GetService<HomeVM>();
        }
    }
}
