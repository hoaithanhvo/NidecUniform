using LiveCharts.Maps;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using NidecUniform.Helpers;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using NidecUniform.Views;
using NidecUniform.Views.Common;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NidecUniform.ViewModels
{
    public class ImportVM : ViewModelBase
    {
        private ObservableCollection<RawData> _dgvImportRequest;
        public ObservableCollection<RawData> DgvImportRequest
        {
            get => _dgvImportRequest;
            set
            {
                _dgvImportRequest = value;
                OnPropertyChanged();
            }
        }

        public ICommand ImportCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        private int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                OnPropertyChanged();
            }
        }

        private readonly IRawData _rawDataRepository;
        private readonly IRequest _requestRepository;
        private readonly IRequestDetails _requestDetailsRepository;
        private readonly IEmpoloyee _employeeRepository;
        private readonly IProduct _productRepository;
        private readonly IUIServices _uiServices;

        #region Variable


        private List<RawData> listRawData = new List<RawData>();

        #endregion
        public ImportVM(IUIServices uiServices)
        {
            ClearCommand = new RelayCommand(_ => ClearDgv());
            OpenCommand = new RelayCommand(_ => ExecuteOpen());
            ImportCommand = new AsyncRelayCommand(() => ExecuteImportAsyc());
            DgvImportRequest = new ObservableCollection<RawData>();
            _rawDataRepository = AppServices.GetService<IRawData>();
            _requestRepository = AppServices.GetService<IRequest>();
            _requestDetailsRepository = AppServices.GetService<IRequestDetails>();
            _employeeRepository = AppServices.GetService<IEmpoloyee>();
            _productRepository = AppServices.GetService<IProduct>();
            _uiServices = uiServices;
        }
        private void ClearDgv()
        {
            DgvImportRequest.Clear();
            TotalCount = 0;
        }
        private async Task<bool> CheckUserInvalidAsync()
        {
            List<string> errorList = new List<string>();
            var existingEmployeeIds = await _employeeRepository.GetAllEmployeeIdsAsync();
            foreach (var rawData in DgvImportRequest)
            {
                if (!existingEmployeeIds.Contains(rawData.EmployeeID))
                {
                    errorList.Add(rawData.No + " - " + rawData.EmployeeID + " - " + rawData.FullName);
                }
            }
            if (errorList.Any())
            {
                MessageBox.Show(string.Join("\n", errorList), "Invalid Employee List", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private async Task<bool> CheckProductInvalidAsync()
        {
            List<string> errorList = new List<string>();
            // Lấy danh sách sản phẩm từ database
            var productList = await _productRepository.GetProductList();

            // Chuyển danh sách productList thành HashSet để tra cứu nhanh hơn
            var validProducts = new HashSet<string>(productList.Select(p => p.ProductEnglishName), StringComparer.OrdinalIgnoreCase);

            // Kiểm tra từng dòng dữ liệu
            foreach (var item in listRawData)
            {
                List<string> invalidFields = new List<string>();

                if (!string.IsNullOrWhiteSpace(item.PaintType) && !validProducts.Contains(item.PaintType))
                    invalidFields.Add($"PaintType: {item.PaintType}");

                if (!string.IsNullOrWhiteSpace(item.ShirtsType) && !validProducts.Contains(item.ShirtsType))
                    invalidFields.Add($"ShirtsType: {item.ShirtsType}");

                if (!string.IsNullOrWhiteSpace(item.ShoesType) && !validProducts.Contains(item.ShoesType))
                    invalidFields.Add($"ShoesType: {item.ShoesType}");

                if (!string.IsNullOrWhiteSpace(item.ConesType) && !validProducts.Contains(item.ConesType))
                    invalidFields.Add($"ConesType: {item.ConesType}");

                // Nếu có lỗi, thêm vào danh sách lỗi
                if (invalidFields.Any())
                {
                    errorList.Add($"EmployeeID: {item.EmployeeID} - FullName: {item.FullName} - " + string.Join(", ", invalidFields));
                }
            }
            if (errorList.Any())
            {
                MessageBox.Show(string.Join("\n", errorList), "Invalid Employee List", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true; // Tất cả đều hợp lệ
        }

        private void ExecuteOpen()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx",
                Title = "Chọn file Excel để import"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProcessExcelFile(openFileDialog.FileName);
            }

        }
        private void ProcessExcelFile(string filePath)
        {
            try
            {
                var package = new ExcelPackage(new FileInfo(filePath));
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Phát ĐP"];
                if (worksheet == null)
                {
                    MessageBox.Show("Không tìm thấy đúng tên");
                    return;
                }
                for (int i = worksheet.Dimension.Start.Row + 7; i <= worksheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        var rawData = ParseRowData(worksheet, i);
                        DgvImportRequest.Add(rawData);
                        TotalCount = DgvImportRequest.Count();
                        listRawData.Add(rawData);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi đọc dữ liệu: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private RawData ParseRowData(ExcelWorksheet worksheet, int rowIndex)
        {
            return new RawData
            {
                No = worksheet.Cells[rowIndex, 1].Value.ToInt(),
                EmployeeID = worksheet.Cells[rowIndex, 2].Text,
                FullName = worksheet.Cells[rowIndex, 3].Text,
                Dept = worksheet.Cells[rowIndex, 4].Text,
                UniformType = worksheet.Cells[rowIndex, 6].Text,
                StartDate = worksheet.Cells[rowIndex, 7].Value.ToDateOnly(),
                EndDate = worksheet.Cells[rowIndex, 8].Value.ToDateOnly(),
                NumberOfPaint = worksheet.Cells[rowIndex, 9].Text.ToInt(),
                PaintType = worksheet.Cells[rowIndex, 10].Text,
                NumberOfshirts = worksheet.Cells[rowIndex, 12].Text.ToInt(),
                ShirtsType = worksheet.Cells[rowIndex, 13].Text,
                NumberOfCones = worksheet.Cells[rowIndex, 15].Text.ToInt(),
                ConesType = worksheet.Cells[rowIndex, 16].Text,
                NumberOfShoes = worksheet.Cells[rowIndex, 17].Text.ToInt(),
                ShoesType = worksheet.Cells[rowIndex, 18].Text,
            };
        }

        private async Task ExecuteImportAsyc()
        {
            try
            {
                if (!await CheckUserInvalidAsync())
                {
                    return;
                }
                if (!await CheckProductInvalidAsync())
                {
                    return;

                }
                _uiServices.ShowProgressDialog();
                await _rawDataRepository.ImportListRequest(listRawData);
                var productList = await _productRepository.GetProductList();
                await ProcessRequests(productList);
                _uiServices.HideProgressDialog();
                MessageBox.Show("Import Success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task ProcessRequests(List<M_Product> productList)
        {
            try
            {
                foreach (var rawData in DgvImportRequest)
                {
                    var request = new M_Request
                    {
                        EmployeeID = rawData.EmployeeID,
                        StartDate = rawData.StartDate,
                        EndDate = rawData.EndDate,
                        RequestType = rawData.UniformType ?? "",
                    };

                    int realRequestID = await _requestRepository.SaveRequest(request);

                    var requestDetails = new List<RequestDetail>();
                    if (rawData.NumberOfPaint > 0)
                    {
                        requestDetails.Add(new RequestDetail
                        {
                            RequestID = realRequestID,
                            ProductID = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.PaintType)?.ProductID ?? "",
                            ProductName = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.PaintType)?.ProductEnglishName ?? "",
                            QuantityRequested = rawData.NumberOfPaint,
                            Unit = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.PaintType)?.Unit ?? ""
                        });
                    }

                    if (rawData.NumberOfshirts > 0)
                    {
                        requestDetails.Add(new RequestDetail
                        {
                            RequestID = realRequestID,
                            ProductID = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ShirtsType)?.ProductID ?? "",
                            ProductName = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ShirtsType)?.ProductEnglishName ?? "",
                            QuantityRequested = rawData.NumberOfshirts,
                            Unit = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ShirtsType)?.Unit ?? ""
                        });
                    }
                    if (rawData.NumberOfCones > 0)
                    {
                        requestDetails.Add(new RequestDetail
                        {
                            RequestID = realRequestID,
                            ProductID = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ConesType)?.ProductID ?? "",
                            ProductName = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ConesType)?.ProductEnglishName ?? "",
                            QuantityRequested = rawData.NumberOfCones,
                            Unit = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ConesType)?.Unit ?? ""
                        });
                    }
                    if (rawData.NumberOfShoes > 0)
                    {
                        requestDetails.Add(new RequestDetail
                        {
                            RequestID = realRequestID,
                            ProductID = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ShoesType)?.ProductID ?? "",
                            ProductName = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ShoesType)?.ProductEnglishName ?? "",
                            QuantityRequested = rawData.NumberOfShoes,
                            Unit = productList.FirstOrDefault(p => p.ProductEnglishName == rawData.ShoesType)?.Unit ?? ""
                        });
                    }
                    await _requestDetailsRepository.ImportRequestDetailsListAsync(requestDetails);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
