using LiveCharts;
using LiveCharts.Wpf;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Repositories.Interface.Common;
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

        private ObservableCollection<ProductModel> _inforCard;
        public ObservableCollection<ProductModel> InforCard
        {
            get { return _inforCard; }
            set
            {
                _inforCard = value;
                OnPropertyChanged(nameof(InforCard));
            }
        }

        private string _bdTotalAmountD;
        public string TotalAmount
        {
            get => _bdTotalAmountD;
            set
            {
                _bdTotalAmountD = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }


        private string _bdTTotalUser;
        public string TotalUser
        {
            get => _bdTTotalUser;
            set
            {
                _bdTTotalUser = value;
                OnPropertyChanged(nameof(TotalUser));
            }
        }
        public ChartValues<int> ChartValues { get; set; }
        public List<string> ProductLabels { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        #endregion

        public HomeVM(IUIServices uiServices)
        {
            _commonRepository = AppServices.GetService<ICommon>();
            _uiServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));
            LoadProducts();
            LoadTooltips();
            LoadChartData();
            LoadTotalAmount();
            LoadTotalUser();
        }

        private void LoadTotalAmount()
        {
            TotalAmount = _commonRepository.GetTotalAmount().ToString("#,##");
        }
        private void LoadTotalUser()
        {
            TotalUser = _commonRepository.GetTotalUser().ToString();
        }

        private void LoadProducts()
        {
            var getProductList = _commonRepository.GetProductInfo();
            Products = new ObservableCollection<ProductModel>(getProductList);
            InforCard = new ObservableCollection<ProductModel>(getProductList);
        }

        private  void LoadTooltips()
        {
            var result =  _commonRepository.GetTooltipsAsync();
            ChartValues = new ChartValues<int>(result.Select(x => x.TotalDeliveries));
            ProductLabels = result.Select(x => x.Department).ToList();
        }

        private  void LoadChartData()
        {
            var result =  _commonRepository.getDataPieChart();
            SeriesCollection = new SeriesCollection();

            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    SeriesCollection.Add(new PieSeries
                    {
                        Title = item.Name,
                        Values = new ChartValues<double> {item.TotalPrice ?? 0 },
                        DataLabels = true,
                        LabelPoint = chartPoint => $"{chartPoint.Y:#,##0}"

                    });
                }
            }
        }

        private async void LoadTooltips()
        {
            var result  =await _employeeRepository.GetTooltipsAsync();
            ChartValues = new ChartValues<int>(result.Select(x => x.TotalDeliveries));
            ProductLabels = result.Select(x => x.Department).ToList();
        }
    }
}
