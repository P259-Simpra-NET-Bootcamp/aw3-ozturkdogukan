using Aw3.Data.Context;
using Aw3.Data.Model;
using Aw3.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Aw3.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        private bool disposed = false;
        public UnitOfWork(Aw3DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(dbContext);
        }

        public int SaveChanges()
        {
            try
            {
                using (TransactionScope tScope = new TransactionScope())
                {
                    int result = dbContext.SaveChanges();
                    tScope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
