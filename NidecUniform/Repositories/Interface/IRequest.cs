using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories
{
    interface IRequest
    {
        void ImportRequest(List<M_Request> ImportRequestList);
        int SaveRequest(M_Request request);
    }
}
