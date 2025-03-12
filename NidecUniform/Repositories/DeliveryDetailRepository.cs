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
    }
}
