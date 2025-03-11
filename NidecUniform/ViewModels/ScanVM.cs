using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace NidecUniform.ViewModels
{
    public class ScanVM : ViewModelBase
    {
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
        #region
        public ICommand SearchCommand { get; set; }

        private readonly IEmpoloyee _employeeRepository;
        private readonly IUIServices _uiServices;

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
            SearchCommand = new RelayCommand(_ => SearchUser());
            _employeeRepository = AppServices.GetService<IEmpoloyee>();
            _uiServices = uiServices;

        }
        private async void SearchUser()
        {
            _uiServices.ShowProgressDialog();
            try
            {
                var User = await _employeeRepository.GetEmployee(BDSearch);
                if (User == null)
                {
                    ClearBindingData();
                    MessageBox.Show($"User {BDSearch} Not found", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
                BindingData(User);
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

        private void BindingData(M_Employee User)
        {
            BDEmployeeID = User.EmployeeID;
            BDFullName = User.FullName;
            BDDepartment = User.Department;
            BDSection = User.Position;
            List<RequestDetail> Details = new List<RequestDetail>();
            BDListRequest = new ObservableCollection<RequestDetail>();
            
            foreach (var i in User.Requests)
            {
                foreach (var j in i.RequestDetails)
                {
                    BDListRequest.Add(new RequestDetail { ProductName = j.ProductName, QuantityDelivered = j.QuantityDelivered, QuantityRequested = j.QuantityRequested,StartDate = i.StartDate,EndDate=i.EndDate});
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


    }
}
