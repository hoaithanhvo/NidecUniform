using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public class RawDataRepository : IRawData
    {
        private readonly NidecUniformContext _context;

        public RawDataRepository(NidecUniformContext context) {

            _context = context;
        }
        public async Task ImportListRequest(List<RawData> rawData)
        {
            _context.RawData.AddRange(rawData);
           await _context.SaveChangesAsync();
        }
    }
}
