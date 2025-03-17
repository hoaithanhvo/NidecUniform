using NidecUniform.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface
{
    internal interface IExport
    {
        Task<List<ExportModel>> getListExportAsync();
    }
}
