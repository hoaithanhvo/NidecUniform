using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface
{
    interface IDelivery
    {
        Task<M_Delivery> GetDelivery(int idRequest);
        Task<int> AddDelivery(M_Delivery delivery);
    }
}
