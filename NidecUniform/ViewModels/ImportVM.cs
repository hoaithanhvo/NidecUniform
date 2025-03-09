using LiveCharts.Maps;
using Microsoft.Win32;
using NidecUniform.Helpers;
using NidecUniform.Models;
using NidecUniform.Repositories;
using NidecUniform.Utilities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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



        private readonly IRawDateRepository _rawDataRepository;
        private readonly IRequest _requestRepository;
        private readonly IRequestDetails _requestDetailsRepository;


        #region Variable

        private string filePath = string.Empty;
        private List<RawData> listRawData = new List<RawData>();
        #endregion
        public ImportVM()
        {
            OpenCommand = new RelayCommand(_ => ExecuteOpen());
            ClearCommand = new RelayCommand(_ => ClearDgv());
            ImportCommand = new AsyncRelayCommand(()=>ExecuteImportAsyc());
            DgvImportRequest = new ObservableCollection<RawData>();
            _rawDataRepository = AppServices.GetService<IRawDateRepository>();
            _requestRepository = AppServices.GetService<IRequest>();
            _requestDetailsRepository = AppServices.GetService<IRequestDetails>();
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
                filePath = openFileDialog.FileName;
            }
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
                        var rawData = new RawData
                        {
                            No = worksheet.Cells[i,1].Value.ToInt(),
                            EmployeeID = worksheet.Cells[i, 2].Text,
                            FullName = worksheet.Cells[i, 3].Text,
                            Dept = worksheet.Cells[i, 4].Text,
                            UniformType = worksheet.Cells[i, 6].Text,
                            StartDate = worksheet.Cells[i, 7].Value.ToDateOnly(),
                            EndDate = worksheet.Cells[i, 8].Value.ToDateOnly(),
                            NumberOfPaint = worksheet.Cells[i, 9].Text.ToInt(),
                            PaintType = worksheet.Cells[i, 10].Text,
                            NumberOfshirts = worksheet.Cells[i, 12].Text.ToInt(),
                            ShirtsType = worksheet.Cells[i, 13].Text,
                            NumberOfCones = worksheet.Cells[i, 15].Text.ToInt(),
                            ConesType = worksheet.Cells[i, 16].Text,
                            NumberOfShoes = worksheet.Cells[i, 17].Text.ToInt(),
                            ShoesType = worksheet.Cells[i, 18].Text,
                        };
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
        private void ClearDgv()
        {
            DgvImportRequest.Clear();
            TotalCount = 0;
        }
        private async Task ExecuteImportAsyc()
        {
            try
            {
                await _rawDataRepository.ImportListRequest(listRawData);

                foreach (var rawData in DgvImportRequest)
                {
                    var request = new M_Request
                    {
                        EmployeeID = rawData.EmployeeID,
                        StartDate = rawData.StartDate,
                        EndDate = rawData.EndDate,
                        RequestType = rawData.UniformType,
                        Status = "Pending"
                    };

                    int realRequestID = _requestRepository.SaveRequest(request);

                    var requestDetails = new List<RequestDetail>
                    {
                        new RequestDetail { RequestID = realRequestID, ProductName = rawData.PaintType, QuantityRequested = rawData.NumberOfPaint, Unit = "Bộ", Status = "Pending" },
                        new RequestDetail { RequestID = realRequestID, ProductName = rawData.ShirtsType, QuantityRequested = rawData.NumberOfshirts, Unit = "Cái", Status = "Pending" },
                        new RequestDetail { RequestID = realRequestID, ProductName = rawData.ConesType, QuantityRequested = rawData.NumberOfCones, Unit = "Cái", Status = "Pending" },
                        new RequestDetail { RequestID = realRequestID, ProductName = rawData.ShoesType, QuantityRequested = rawData.NumberOfShoes, Unit = "Đôi", Status = "Pending" }
                    };

                    _requestDetailsRepository.ImportRequestDetailsList(requestDetails);
                }

                MessageBox.Show("Import thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi import dữ liệu: {ex.Message}");
            }
        }
    }
}
