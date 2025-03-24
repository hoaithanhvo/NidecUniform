using CoreScanner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NidecUniform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NidecUniform.Repositories.Interface.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NidecUniformContext _context;
        private IDbContextTransaction _transaction;
        public IRequest RequestRepository { get; }
        public IRequestDetails RequestDetailsRepository { get; }


        public UnitOfWork(NidecUniformContext context,IRequest request,IRequestDetails requestDetails) {
            
            _context = context;
            RequestRepository = request;
            RequestDetailsRepository = requestDetails;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            _transaction?.Commit();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
