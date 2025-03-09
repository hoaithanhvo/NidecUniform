using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public class RequestRepository : IRequest
    {
        private readonly NidecUniformContext _context;

        public RequestRepository(NidecUniformContext context) {
            _context = context;
        }
        public void ImportRequest(List<M_Request> ImportRequestList)
        {
           _context.M_Requests.AddRange(ImportRequestList);
           _context.SaveChanges();
        }
        public int SaveRequest(M_Request request)
        {
            _context.M_Requests.Add(request);
            _context.SaveChanges();
            return request.ID; // Trả về ID sau khi lưu vào DB
        }
    }
}
