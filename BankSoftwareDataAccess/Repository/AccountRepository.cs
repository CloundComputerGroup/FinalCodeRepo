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
    public class AccountRepository : EntityRepositoryBase<Account>, IAccountRepository
    {
        private IUnitOfWork unitOfWork;

        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateAccount(Account model)
        {
            var account = DataContext.Accounts.Count(u => u.AccountPk == model.AccountPk) > 0;
            if (!account)
            {
                model.AccountPk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
           
            unitOfWork.SaveChanges();
            return model.AccountPk;
        }

        public Guid SaveUpdateAccountCreditTransaction(Account model)
        {
            try
            {
                var account = DataContext.Accounts.FirstOrDefault(u => u.AccountPk == model.AccountPk);
                if (account!=null)
                {
                    account.Balance = account.Balance + model.Balance;
                    account.ModDate = DateTime.Now;
                    SetModified(account);
                    unitOfWork.SaveChanges();
                    return account.AccountPk;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {

                return Guid.Empty;
            }
        }

        public Guid SaveUpdateAccountDebitTransaction(List<Account> model)
        {
            try
            {
                Guid resultPk = Guid.Empty;
                foreach (var item in model)
                {
                    var account = DataContext.Accounts.FirstOrDefault(u => u.AccountPk == item.AccountPk);
                    if (account != null )
                    {
                        account.Balance = account.Balance + item.Balance;
                        account.ModDate = DateTime.Now;
                        SetModified(account);
                        unitOfWork.SaveChanges();
                        resultPk = account.AccountPk;


                    }

                }

                return resultPk;
            }
            catch (Exception ex)
            {

                return Guid.Empty;
            }
        }

        public Guid DeleteAccount(Account model)
        {
            var account = DataContext.Accounts.Count(u => u.AccountPk == model.AccountPk && u.IsDeleted != true) > 0;
            if (account)
            {
                SetModified(model);
                unitOfWork.SaveChanges();
            }
            return model.AccountPk;
        }


        public List<Account> GetAllUserAccounts(Guid userId)
        {
            return DataContext.Accounts.Where(x=>x.IsDeleted!=true && x.UserFk == userId).ToList();
        }

        public List<Account> GetAllAccounts()
        {
            return DataContext.Accounts.Where(x => x.IsDeleted != true).ToList();
        }


        public Account GetAccountById(Guid accountId)
        {
            return DataContext.Accounts.Where(x => x.AccountPk == accountId && x.IsDeleted != true).FirstOrDefault();
        }
        


        
    }
}
