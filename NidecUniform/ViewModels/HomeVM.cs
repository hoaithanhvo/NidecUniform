using LiveCharts;
using LiveCharts.Wpf;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NidecUniform.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private readonly IDeliveryDetail _deliveryDetailRepository;
        private readonly IEmpoloyee _employeeRepository;

        private readonly IUIServices _uiServices;

        #region

        private ObservableCollection<ProductModel> _products;
        public ObservableCollection<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
        public ChartValues<int> ChartValues { get; set; }
        public List<string> ProductLabels { get; set; }
        #endregion

        public HomeVM(IUIServices uiServices)
        {
            _deliveryDetailRepository = AppServices.GetService<IDeliveryDetail>();
            _employeeRepository = AppServices.GetService<IEmpoloyee>();

            _uiServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));
            LoadProducts();
            LoadTooltips();
        }

        private void LoadProducts()
        {
            var getProductList = _deliveryDetailRepository.GetProductInfo();
            Products = new ObservableCollection<ProductModel>(getProductList);
        }

        private async void LoadTooltips()
        {
            var result  =await _employeeRepository.GetTooltipsAsync();
            ChartValues = new ChartValues<int>(result.Select(x => x.TotalDeliveries));
            ProductLabels = result.Select(x => x.Department).ToList();
        }
    }
}
