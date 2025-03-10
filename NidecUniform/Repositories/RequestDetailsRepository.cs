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
    }
}
