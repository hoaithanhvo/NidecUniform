using Azure.Core;
using NidecUniform.Helpers;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace NidecUniform.ViewModels
{
    public class ScanVM : ViewModelBase
    {
        public ICommand SearchCommand { get; set; }
        public ICommand IncreaseQuantityCommand { get; private set; }
        public ICommand DecreaseQuantityCommand { get; private set; }

        public ICommand PushCommand { get; set; }

        private readonly IEmpoloyee _employeeRepository;
        private readonly IDelivery _deliveryRepository;
        private readonly IDeliveryDetail _deliveryDetailReporitory;
        private readonly IRequestDetails _requestDetailReporitory;
        private readonly IUIServices _uiServices;


        #region Binding Data
        private string _bdEmployeeID;
        public string BDEmployeeID
        {
            get => _bdEmployeeID;
            set
            {
                _bdEmployeeID = value;
                OnPropertyChanged(nameof(BDEmployeeID));
            }
        }
        private string _bdFullName;
        public string BDFullName
        {
            get => _bdFullName;
            set
            {
                _bdFullName = value;
                OnPropertyChanged(nameof(BDFullName));
            }
        }

        private string _bdDepartment;
        public string BDDepartment
        {
            get => _bdDepartment;
            set
            {
                _bdDepartment = value;
                OnPropertyChanged(nameof(BDDepartment));
            }
        }
        private string _bdSection;
        public string BDSection
        {
            get => _bdSection;
            set
            {
                _bdSection = value;
                OnPropertyChanged(nameof(BDSection));
            }
        }

        private string _bdSearch;
        public string BDSearch
        {
            get => _bdSearch;
            set
            {
                _bdSearch = value;
                OnPropertyChanged(nameof(BDSearch));
            }
        }

        private int _bdQuantity;
        public int BDQuantity
        {
            get => _bdQuantity;
            set
            {
                _bdQuantity = value;
                OnPropertyChanged(nameof(BDQuantity));
            }
        }

        private ObservableCollection<RequestDetail> _bdListRequest;
        public ObservableCollection<RequestDetail> BDListRequest
        {
            get => _bdListRequest;
            set
            {
                _bdListRequest = value;
                OnPropertyChanged(nameof(BDListRequest));
            }
        }

        #endregion 

        public ScanVM(IUIServices uiServices)
        {
            BDListRequest = new ObservableCollection<RequestDetail>();
            _employeeRepository = AppServices.GetService<IEmpoloyee>();
            _deliveryRepository = AppServices.GetService<IDelivery>();
            _deliveryDetailReporitory = AppServices.GetService<IDeliveryDetail>();
            _requestDetailReporitory = AppServices.GetService<IRequestDetails>();

            _uiServices = uiServices;

            SearchCommand = new RelayCommand(_ => SearchUser());
            IncreaseQuantityCommand = new RelayCommand(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand(DecreaseQuantity);
            PushCommand = new RelayCommand(_ => PushRequest());
        }

        private void IncreaseQuantity(object parameter)
        {
            Debug.WriteLine("Increase Quantity clicked");
            if (parameter is RequestDetail item)
            {
                var selectedItem = BDListRequest.FirstOrDefault(x => x.ID == item.ID);
                if (selectedItem != null)
                {
                    int maxQuantity = selectedItem.QuantityRequested - selectedItem.QuantityDelivered;
                    selectedItem.BDQuantity = Math.Min(maxQuantity, (selectedItem.BDQuantity ?? 0) + 1);
                    BDListRequest = new ObservableCollection<RequestDetail>(BDListRequest);
                    OnPropertyChanged(nameof(BDListRequest));
                }
            }
            else
            {
                Debug.WriteLine("Parameter is not a RequestDetail");
            }
        }
        private void DecreaseQuantity(object parameter)
        {
            Debug.WriteLine("Increase Quantity clicked");
            if (parameter is RequestDetail item)
            {
                var selectedItem = BDListRequest.FirstOrDefault(x => x.ID == item.ID);
                if (selectedItem != null)
                {
                    selectedItem.BDQuantity = Math.Max(0, (selectedItem.BDQuantity ?? 0) - 1);
                    BDListRequest = new ObservableCollection<RequestDetail>(BDListRequest);
                    OnPropertyChanged(nameof(BDListRequest));
                }
            }
            else
            {
                Debug.WriteLine("Parameter is not a RequestDetail");
            }
        }

        private M_Employee User = new M_Employee();
        private async void SearchUser()
        {
            BDListRequest.Clear();
            _uiServices.ShowProgressDialog();
            try
            {
                User = await _employeeRepository.GetEmployee(BDSearch.Trim());
                if (User == null)
                {
                    ClearBindingData();
                    MessageBox.Show($"User {BDSearch} Not found", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
                BindingData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            finally
            {
                _uiServices.HideProgressDialog();
            }

        }

        private void BindingData()
        {
            BDEmployeeID = User.EmployeeID;
            BDFullName = User.FullName;
            BDDepartment = User.Department;
            BDSection = User.Position;


            foreach (var i in User.Requests)
            {
                foreach (var j in i.RequestDetails)
                {
                    BDListRequest.Add(new RequestDetail { ID = j.ID, ProductName = j.ProductName, 
                        QuantityDelivered = j.QuantityDelivered, QuantityRequested = j.QuantityRequested, 
                        StartDate = i.StartDate, EndDate = i.EndDate, RequestID = i.ID ,ProductID = j.ProductID,EmployeeID = j.EmployeeID,Unit=j.Unit

                    });
                }
            }
        }
        private void ClearBindingData()
        {
            BDEmployeeID = string.Empty;
            BDFullName = string.Empty;
            BDDepartment = string.Empty;
            BDSection = string.Empty;
        }

        private async Task PushRequest()
        {
            try
            {
                var deliveryTasks = new List<Task>();
                var detailTasks = new List<Task>();

                List<RequestDetail> BDListRequestTemp = new List<RequestDetail>();
                List<DeliveryDetail> deliveryDetailsList = new List<DeliveryDetail>();
                List<RequestDetail> updateQuantityDelivered = new List<RequestDetail>();
                foreach (var item in BDListRequest.Where(i => i.BDQuantity > 0))
                {
                    BDListRequestTemp.Add(item);
                }

                foreach (var request in User.Requests)
                {
                    foreach (var item in BDListRequestTemp)
                    {
                        if (request.ID == item.RequestID)
                        {
                            var getDelivery = await _deliveryRepository.GetDelivery(request.ID);
                            int deliveryID;

                            if (getDelivery == null)
                            {
                                deliveryID = await _deliveryRepository.AddDelivery(new M_Delivery
                                {
                                    RequestID = request.ID,
                                    EmployeeID = request.EmployeeID,
                                });
                            }
                            else
                            {
                                deliveryID = getDelivery.ID;
                            }
                            deliveryDetailsList.Add(new DeliveryDetail
                            {
                                EmployeeID = item.EmployeeID,
                                ProductID = item.ProductID,
                                DeliveryID = deliveryID,
                                ProductName = item.ProductName,
                                QuantityDelivered = (int)item.BDQuantity,
                                Unit = item.Unit,
                                RequestID = request.ID,
                                
                            });
                            updateQuantityDelivered.Add(new RequestDetail
                            {
                                ID = item.ID,
                                QuantityDelivered = item.BDQuantity ??0,
                            });
                        }
                    }
                }
                foreach (var request in updateQuantityDelivered)
                {
                    await _requestDetailReporitory.UpdateQuantityDelivered(request.ID,request.QuantityDelivered);
                }
                await _deliveryDetailReporitory.AddDeliveryDetails(deliveryDetailsList);
                MessageBox.Show("Import Sucess", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
