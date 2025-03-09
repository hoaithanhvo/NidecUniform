using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    interface IRequestDetails
    {
        void ImportRequestDetailsList(List<RequestDetail> requestDetailsList);
    }
}
