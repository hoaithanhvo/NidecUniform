using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    public interface IRawData
    {
<<<<<<<< HEAD:NidecUniform/Repositories/Interface/IEmpoloyeeRepository.cs
        M_Employee GetEmployee(int id);

        Task importEmployee(List<M_Employee> employees);
========
        Task ImportListRequest(List<RawData> rawData);
>>>>>>>> origin/Phase2:NidecUniform/Repositories/Interface/IRawData.cs
    }
}
