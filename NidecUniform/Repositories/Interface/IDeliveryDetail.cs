using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface
{
    interface IDeliveryDetail
    {
        Task AddDeliveryDetails(List<DeliveryDetail> deliveryDetail);
        List<ProductModel> GetProductInfo();
    }
}
