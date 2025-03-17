using Microsoft.EntityFrameworkCore;
using NidecUniform.Models;
using NidecUniform.Models.Model;
using NidecUniform.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public class ExportRepository : IExport
    {
        private readonly NidecUniformContext _context;

        public ExportRepository(NidecUniformContext context)
        {
            _context = context;
        }
        public async Task<List<ExportModel>> getListExportAsync()
        {
            var result = await (from deliveryDetails in _context.DeliveryDetails
                          join request in _context.M_Requests on deliveryDetails.RequestID equals request.ID
                          join employee in _context.M_Employees on request.EmployeeID equals employee.EmployeeID
                          select new ExportModel
                          {
                              EmployeeID = employee.EmployeeID,
                              FullName = employee.FullName,
                              Department = employee.Department,
                              Position = employee.Position,
                              RequestType = request.RequestType,
                              ProductID = deliveryDetails.ProductID,
                              ProductName = deliveryDetails.ProductName,
                              QuantityDelivered = deliveryDetails.QuantityDelivered,
                              CreateDate = deliveryDetails.CreateDate
                          }).OrderByDescending(s=>s.CreateDate).ToListAsync();
            return result;
        }
    }
}
