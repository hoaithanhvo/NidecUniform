using LiveCharts;
using LiveCharts.Wpf;
using NidecUniform.Helpers;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Repositories.Interface.Common;
using NidecUniform.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NidecUniform.ViewModels
{
    public class HomeVM : ViewModelBase
    {
        private readonly ICommon _commonRepository;

        private readonly IUIServices _uiServices;

        

        #region binding

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


        private DateTime? _startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _endDate =  DateTime.Today.AddDays(1).AddTicks(-1);
        public DateTime? EndDate 
        {
            get => _endDate;
            set
            {
                if (value.HasValue)
                {
                    // Giữ nguyên ngày nhưng cập nhật thời gian thành cuối ngày (23:59:59.999)
                    _endDate = value.Value.Date.AddDays(1).AddTicks(-1);
                }
                else
                {
                    _endDate = null;
                }
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public ICommand SearchCommand { get; }
        #endregion

        public HomeVM(IUIServices uiServices)
        {
            _commonRepository = AppServices.GetService<ICommon>();
            _uiServices = uiServices;
            SearchCommand = new RelayCommand(_ => ExecuteSearch());
            RenderData();
        }

        private void RenderData()
        {
            _uiServices.ShowProgressDialog();
            LoadProducts();
            LoadTooltips();
            LoadChartData();
            LoadTotalAmount();
            LoadTotalUser();
            _uiServices.HideProgressDialog();

        }
        private void ExecuteSearch()
        {
            RenderData();
        }
        private void LoadTotalAmount()
        {
            TotalAmount = _commonRepository.GetTotalAmount(StartDate.Value, EndDate.Value).ToString("#,##");
        }
        private void LoadTotalUser()
        {
            TotalUser = _commonRepository.GetTotalUser(StartDate.Value, EndDate.Value).ToString();
        }

        private void LoadProducts()
        {
            var getProductList = _commonRepository.GetProductInfo(StartDate.Value, EndDate.Value);
            Products = new ObservableCollection<ProductModel>(getProductList);
            InforCard = new ObservableCollection<ProductModel>(getProductList);
        }

        private void LoadTooltips()
        {
            var result = _commonRepository.GetTooltipsAsync(StartDate.Value, EndDate.Value);

            // Đảm bảo rằng ChartValues không phải là null trước khi thay đổi dữ liệu
            if (ChartValues == null)
                ChartValues = new ChartValues<int>();
            if(ProductLabels == null)
                ProductLabels = new List<string>();

            // Xóa các giá trị cũ trước khi thêm mới
            ChartValues.Clear();
            ProductLabels.Clear();

            if (result != null && result.Any())
            {
                // Cập nhật dữ liệu cho ChartValues và ProductLabels
                foreach (var item in result)
                {
                    ChartValues.Add(item.TotalDeliveries);
                }

               ProductLabels.AddRange(result.Select(x => x.Department).ToList());
            }
        }

        private void LoadChartData()
        {
            var result = _commonRepository.getDataPieChart(StartDate.Value, EndDate.Value);

            if (SeriesCollection == null)
                SeriesCollection = new SeriesCollection();
            else
                SeriesCollection.Clear();

            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    SeriesCollection.Add(new PieSeries
                    {
                        Title = item.Name,
                        Values = new ChartValues<double> { item.TotalPrice ?? 0 },
                        DataLabels = true,
                        LabelPoint = chartPoint => $"{chartPoint.Y:#,##0}"
                    });
                }
            }
        }
    }
}
