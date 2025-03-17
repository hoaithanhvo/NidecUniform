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

        public ExportVM(IUIServices uiServices)
        {
            _exportRepository = AppServices.GetService<IExport>();
            getDataDelivery();
            DgvExportRequest = new ObservableCollection<ExportModel>();


        }

        private async Task getDataDelivery()
        {
            var result = await _exportRepository.getListExportAsync();
            TotalCount = result.Count;
            foreach (var exportModel in result)
            {
                DgvExportRequest.Add(exportModel);
            }

        }
    }
}
