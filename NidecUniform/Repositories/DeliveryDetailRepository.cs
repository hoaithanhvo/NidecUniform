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
    public class DeliveryDetailRepository : IDeliveryDetail
    {
        private readonly NidecUniformContext _context;

        public DeliveryDetailRepository(NidecUniformContext context)
        {
            _context = context;
        }
        public async Task AddDeliveryDetails(List<DeliveryDetail> deliveryDetail)
        {
            await _context.AddRangeAsync(deliveryDetail);
            await _context.SaveChangesAsync();
        }



        public List<ProductModel> GetProductInfo()
        {
            var result =  (from d in _context.DeliveryDetails
                                join p in _context.M_Products on d.ProductID equals p.ProductID
                                group d by new { d.ProductID, p.ProductName, p.Price } into g
                                select new ProductModel
                                {
                                    ProductName = g.Key.ProductName,
                                    Price = g.Key.Price,
                                    Quantity = g.Sum(d => d.QuantityDelivered),
                                    TotalPrice = g.Key.Price * g.Sum(d => d.QuantityDelivered),
                                    Image = $"/Images/{g.Key.ProductID}.png"
                                }).ToList();
            return result;
        }
    }
}
