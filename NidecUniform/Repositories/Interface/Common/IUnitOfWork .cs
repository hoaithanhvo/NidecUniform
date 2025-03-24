using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IRequest RequestRepository { get; }
        IRequestDetails RequestDetailsRepository { get; }

        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
