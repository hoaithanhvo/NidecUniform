using Microsoft.Win32;
using NidecUniform.Helpers;
using NidecUniform.Models;
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
        public ObservableCollection<RawData> Items { get; set; }
        public ICommand ImportCommand { get; set; }
        public ImportVM()
        {
            ImportCommand = new RelayCommand(_ => ExecuteImport());
            Items = new ObservableCollection<RawData>();
        }
        private string filePath;
        private void ExecuteImport()
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
            List<RawData> listRawData = new List<RawData>();
            try
            {
                listRawData.Clear();
                var package = new ExcelPackage(new FileInfo(filePath));
                var worksheetCount = package.Workbook.Worksheets.Count;
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
                        string sttNo = worksheet.Cells[i, 1].Text;
                        string msnvID = worksheet.Cells[i, 2].Text;
                        string fullName = worksheet.Cells[i, 3].Text;
                        string dept = worksheet.Cells[i, 4].Text;
                        string gender = worksheet.Cells[i, 5].Text;
                        string uniformType = worksheet.Cells[i, 6].Text;
                        DateTime startDate = worksheet.Cells[i, 7].Value.ToDateTime();
                        DateTime endDate = worksheet.Cells[i, 8].Value.ToDateTime();

                        int numberOfPaint = worksheet.Cells[i, 9].Value as int? ?? 0;
                        string paintType = worksheet.Cells[i, 10].Text;
                        int numberOfshirts = worksheet.Cells[i, 12].Value as int? ?? 0;
                        string shirtsType = worksheet.Cells[i, 13].Text;
                        string numberOfCones = worksheet.Cells[i, 14].Text;
                        string conesType = worksheet.Cells[i, 15].Text;
                        int numberOfShoes = worksheet.Cells[i, 16].Value as int? ?? 0;
                        string shoesType = worksheet.Cells[i, 17].Text;
                        bool signReceived = worksheet.Cells[i, 18].Value as bool? ?? true;

                        RawData data = new RawData();
                        data.ID = Int32.Parse(sttNo);
                        data.EmployeeID = msnvID;
                        data.FullName = fullName;
                        data.Dept = dept;
                        data.Gender = gender;
                        data.UniformType = uniformType;
                        data.StartDate = startDate;
                        data.EndDate = endDate;
                        data.NumberOfPaint = numberOfPaint;
                        data.PaintType = paintType;
                        data.NumberOfshirts = numberOfshirts;
                        data.ShirtsType = shirtsType;
                        data.ConesType = conesType;
                        data.NumberOfShoes = numberOfShoes;
                        data.ShoesType = shoesType;
                        data.SignReceived = signReceived;
                        listRawData.Add(data);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex) { }

            if (Items != null)
            {
                Items.Clear();
                foreach (var item in listRawData)
                {
                    Items.Add(item);
                }
            }
        }
    }
}
