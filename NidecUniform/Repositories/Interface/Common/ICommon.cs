using NidecUniform.Models;
using NidecUniform.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface.Common
{
    public interface ICommon
    {
        List<PieModel> getDataPieChart(DateTime startDate, DateTime endDate);
        List<ProductModel> GetProductInfo(DateTime startDate , DateTime endDate);
        List<TooltipsModel> GetTooltipsAsync(DateTime startDate, DateTime endDate);
        int GetTotalUser(DateTime startDate, DateTime endDate);

        decimal GetTotalAmount(DateTime startDate, DateTime endDate);
    }
}
