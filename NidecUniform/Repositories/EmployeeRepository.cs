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
        public M_Employee GetEmployee(int id)
        {
            return _context.M_Employees.Find(id);
        }

        public async Task importEmployee(List<M_Employee> employees)
        {
            _context.M_Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }
    }
}
