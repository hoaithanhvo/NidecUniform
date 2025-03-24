using NidecUniform.Models;
using NidecUniform.Models.Model;
using NidecUniform.Repositories;
using NidecUniform.Repositories.Interface;
using NidecUniform.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NidecUniform.ViewModels
{
    public class ExportVM : ViewModelBase
    {
        #region Binding
        private ObservableCollection<ExportModel> _dgvExportRequest;
        public ObservableCollection<ExportModel> DgvExportRequest
        {
            get => _dgvExportRequest;
            set
            {
                _dgvExportRequest = value;
                OnPropertyChanged();
            }
        }

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
        #endregion

        private readonly IExport _exportRepository;
        private readonly IUIServices _uiServices;

        public ExportVM()
        {
            _exportRepository = AppServices.GetService<IExport>();
            DgvExportRequest = new ObservableCollection<ExportModel>();
        }

        public async Task getDataDelivery()
        {
            try
            {
                DgvExportRequest.Clear();
                await Task.Delay(3000);
                var result = await _exportRepository.getListExportAsync();
                if (result != null)
                {
                    foreach (var exportModel in result)
                    {
                        DgvExportRequest.Add(exportModel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.InnerException?.Message, "Error", MessageBoxButton.OK);
            }
        }
    }
}
