using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface
{
    public interface IProduct
    {
        Task<List<M_Product>> GetProductList();
    }
}
