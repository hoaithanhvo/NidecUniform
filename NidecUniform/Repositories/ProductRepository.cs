using Microsoft.EntityFrameworkCore;
using NidecUniform.Models;
using NidecUniform.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly NidecUniformContext _context;

        public ProductRepository(NidecUniformContext context)
        {
            _context = context;
        }
        public async Task<List<M_Product>> GetProductList()
        {
            return await _context.M_Product.ToListAsync();
        }
    }
}
