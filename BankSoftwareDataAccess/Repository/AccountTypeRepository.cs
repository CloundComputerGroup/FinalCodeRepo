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
    public class AccountTypeRepository : EntityRepositoryBase<AccountType>, IAccountTypeRepository
    {
        private IUnitOfWork unitOfWork;

        public AccountTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateAccountType(AccountType model)
        {
            var account = DataContext.AccountTypes.Count(u => u.AccountTypePk == model.AccountTypePk) > 0;
            if (!account)
            {
                model.AccountTypePk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.AccountTypePk;
        }


        public List<AccountType> GetAllAccountType()
        {
            return DataContext.AccountTypes.ToList();
        }
        
        
        public AccountType GetAccountTypeById(Guid accountTypeId)
        {
            return DataContext.AccountTypes.Where(x => x.AccountTypePk == accountTypeId).FirstOrDefault();
        }
        


        
    }
}
