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
    public class TransactionTypeRepository : EntityRepositoryBase<TransactionType>, ITransactionTypeRepository
    {
        private IUnitOfWork unitOfWork;

        public TransactionTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateTransactionType(TransactionType model)
        {
            var account = DataContext.TransactionTypes.Count(u => u.TransactionTypePk == model.TransactionTypePk) > 0;
            if (!account)
            {
                model.TransactionTypePk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.TransactionTypePk;
        }


        public List<TransactionType> GetAllTransactionType()
        {
            return DataContext.TransactionTypes.ToList();
        }
        
        
        public TransactionType GetTransactionTypeById(Guid transactionTypeId)
        {
            return DataContext.TransactionTypes.Where(x => x.TransactionTypePk == transactionTypeId).FirstOrDefault();
        }
        


        
    }
}
