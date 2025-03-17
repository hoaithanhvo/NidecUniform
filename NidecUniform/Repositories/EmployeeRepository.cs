using Microsoft.EntityFrameworkCore;
using NidecUniform.Migrations;
using NidecUniform.Models;
using NidecUniform.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public class EmployeeRepository : IEmpoloyee
    {
        private readonly NidecUniformContext _context;

        public EmployeeRepository(NidecUniformContext context)
        {

            _context = context;
        }

        public async Task<HashSet<string>> GetAllEmployeeIdsAsync()
        {
            return await _context.M_Employees.Select(s => s.EmployeeID).ToHashSetAsync();
        }

        public async Task<M_Employee> GetEmployee(string employeeID)
        {
            return await _context.M_Employees.Include(s => s.Requests).ThenInclude(r=>r.RequestDetails).ThenInclude(d=>d.DeliveryDetails).Where(s=>s.EmployeeID == employeeID).FirstOrDefaultAsync();

        }

       

        public async Task importEmployee(List<M_Employee> employees)
        {
            _context.M_Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TooltipsModel>> GetTooltipsAsync()
        {
            var result = await(from emp in _context.M_Employees
                               join del in _context.M_Deliveries
                               on emp.EmployeeID equals del.EmployeeID
                               group emp by emp.Department into grouped
                               select new TooltipsModel
                               {
                                   Department = grouped.Key,
                                   TotalDeliveries = grouped.Count()
                               }).ToListAsync();

            return result;
        }
    }
}
