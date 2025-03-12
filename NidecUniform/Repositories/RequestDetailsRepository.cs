using Microsoft.EntityFrameworkCore;
using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    class RequestDetailsRepository : IRequestDetails
    {
        private readonly NidecUniformContext _context;

        public RequestDetailsRepository(NidecUniformContext context)
        {
            _context = context;
        }
        public async Task ImportRequestDetailsListAsync(List<RequestDetail> requestDetailsList)
        {
            await _context.RequestDetails.AddRangeAsync(requestDetailsList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantityDelivered(int requestDetailsID, int? quantityDelivered)
        {
            var requestDetail = await _context.RequestDetails
                .FirstOrDefaultAsync(s => s.ID == requestDetailsID);

            if (requestDetail != null && quantityDelivered.HasValue)
            {
                requestDetail.QuantityDelivered = quantityDelivered.Value; // Tránh lỗi khi giá trị là null
                await _context.SaveChangesAsync(); // Lưu thay đổi vào DB
            }
        }

    }
}
