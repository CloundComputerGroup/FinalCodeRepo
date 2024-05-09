using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess
{
    public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly IBankContext dbContext;

        public UnitOfWork()
        {
            this.dbContext = new BankContext();
        }

        public void Dispose()
        {
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public IBankContext Context
        {
            get { return dbContext; }
        }
    }
}
