using Microsoft.EntityFrameworkCore;
using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    class EmployeeRepository : IEmpoloyeeRepository
    {
        private readonly NidecUniformContext _context;

        public EmployeeRepository(NidecUniformContext context) {

            _context = context;
        }
        public MEmployee GetEmployee(int id)
        {
            return _context.MEmployees.Find(id);
        }
    }
}
