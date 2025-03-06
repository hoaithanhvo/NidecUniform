using Microsoft.Win32;
using NidecUniform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NidecUniform.ViewModels
{
    public class ImportVM : ViewModelBase
    {
        public ICommand ImportCommand { get; set; }
        public ImportVM()
        {
            ImportCommand = new RelayCommand(_ => ExecuteImport());
        }

        private void ExecuteImport()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx",
                Title = "Chọn file Excel để import"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
            }
        }
    }
}
