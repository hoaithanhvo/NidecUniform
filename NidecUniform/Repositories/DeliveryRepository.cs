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
    public class DeliveryRepository : IDelivery
    {
        private readonly NidecUniformContext _context;

        public DeliveryRepository(NidecUniformContext context)
        {
            _context = context;
        }

        public async Task<int> AddDelivery(M_Delivery delivery)
        {
            await _context.M_Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
            return delivery.ID;
        }

        public async Task<M_Delivery> GetDelivery(int idRequest)
        {
            return await _context?.M_Deliveries.FirstOrDefaultAsync(s => s.RequestID == idRequest);
        }


    }
}
