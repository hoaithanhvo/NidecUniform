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

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Export(object obj) => CurrentView = new ExportVM();
        private void Import(object obj) => CurrentView = new ImportVM();
        private void Scan(object obj) => CurrentView = new ScanVM();
       

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            ExportCommand = new RelayCommand(Export);
            ImportCommand = new RelayCommand(Import);
            ScanCommand = new RelayCommand(Scan);
            CurrentView = new HomeVM();
        }
    }
}
