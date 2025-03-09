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
        public void ImportRequestDetailsList(List<RequestDetail> requestDetailsList)
        {
            _context.RequestDetails.AddRange(requestDetailsList);
            _context.SaveChanges();
        }
    }
}
