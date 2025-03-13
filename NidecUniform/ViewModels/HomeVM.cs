using NidecUniform.Models;
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
        private readonly IUIServices _uiServices;
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


        public HomeVM(IUIServices uiServices)
        {
            _deliveryDetailRepository = AppServices.GetService<IDeliveryDetail>();
            _uiServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));
            LoadProducts();
        }

        private void LoadProducts()
        {
            var getProductList = _deliveryDetailRepository.GetProductInfo();
            Products = new ObservableCollection<ProductModel>(getProductList);
        }
    }
}
