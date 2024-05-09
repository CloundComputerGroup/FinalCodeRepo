using BankSoftwareDataAccess.EntityRepository;
using BankSoftwareDataAccess.IRepository;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Repository
{
    public class TransactionRepository : EntityRepositoryBase<Transaction>, ITransactionRepository
    {
        private IUnitOfWork unitOfWork;

        public TransactionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateTransaction(Transaction model)
        {
            var account = DataContext.Transactions.Count(u => u.TransactionPk == model.TransactionPk) > 0;
            if (!account)
            {
                model.TransactionPk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.TransactionPk;
        }


        public List<Transaction> GetAllTransaction(Guid accountId)
        {
            return DataContext.Transactions.Where(x=>x.AccountFk == accountId).ToList();
        }
        
        
        public Transaction GetTransactionById(Guid resourceId)
        {
            return DataContext.Transactions.Where(x => x.TransactionPk == resourceId).FirstOrDefault();
        }
        


        
    }
}
