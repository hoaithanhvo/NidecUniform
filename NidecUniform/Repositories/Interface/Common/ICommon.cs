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
        List<PieModel> getDataPieChart();
        List<ProductModel> GetProductInfo();
        List<TooltipsModel> GetTooltipsAsync();
        int GetTotalUser();

        decimal GetTotalAmount();
        

    }
}
