using Microsoft.EntityFrameworkCore;
using NidecUniform.Models;
using NidecUniform.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface.Common
{
    public class CommonRepository : ICommon
    {
        private readonly NidecUniformContext _context;

        public CommonRepository(NidecUniformContext context)
        {

            _context = context;
        }
        public List<PieModel> getDataPieChart()
        {
            var query = from a in _context.M_Requests
                        join b in _context.M_Employees on a.EmployeeID equals b.EmployeeID
                        join c in _context.DeliveryDetails on a.ID equals c.RequestID
                        join d in _context.M_Product on c.ProductID equals d.ProductID
                        group new { d.Price, c.QuantityDelivered } by b.Department into grouped
                        select new PieModel
                        {
                            Name = grouped.Key,
                            TotalPrice = grouped.Sum(x => x.Price * x.QuantityDelivered)
                        };


            return query.ToList();
        }
        public List<TooltipsModel> GetTooltipsAsync()
        {
            var result =  (from emp in _context.M_Employees
                                join del in _context.M_Deliveries
                                on emp.EmployeeID equals del.EmployeeID
                                group emp by emp.Department into grouped
                                select new TooltipsModel
                                {
                                    Department = grouped.Key,
                                    TotalDeliveries = grouped.Count()
                                }).ToList();

            return result;
        }
        public List<ProductModel> GetProductInfo()
        {
            var result = (from d in _context.DeliveryDetails
                          join p in _context.M_Product on d.ProductID equals p.ProductID
                          group d by new { d.ProductID, p.ProductEnglishName, p.Price } into g
                          select new ProductModel
                          {
                              ProductName = g.Key.ProductEnglishName,
                              Price = g.Key.Price,
                              Quantity = g.Sum(d => d.QuantityDelivered),
                              TotalPrice = g.Key.Price * g.Sum(d => d.QuantityDelivered),
                              Image = $"/Images/{g.Key.ProductID}.png"
                          }).ToList();
            return result;
        }

        public int GetTotalUser()
        {
            var result = (from request in _context.M_Requests
                          join employee in _context.M_Employees on request.EmployeeID equals employee.EmployeeID
                          join deliveryDetail in _context.DeliveryDetails on request.ID equals deliveryDetail.RequestID
                          select request.EmployeeID).Distinct().Count();
            return result;
        }

        public decimal GetTotalAmount()
        {
            decimal totalAmount = (from deliveryDetail in _context.DeliveryDetails
                                   join product in _context.M_Product on deliveryDetail.ProductID equals product.ProductID
                                   select Convert.ToDecimal(deliveryDetail.QuantityDelivered) * Convert.ToDecimal(product.Price ?? 0))
                          .Sum();
            return totalAmount;
        }
    }
}
