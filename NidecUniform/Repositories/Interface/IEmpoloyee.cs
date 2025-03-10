using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public interface IEmpoloyee
    {
        M_Employee GetEmployee(int id);

        Task importEmployee(List<M_Employee> employees);

        Task<HashSet<string>> GetAllEmployeeIdsAsync();
    }
}
