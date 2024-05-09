using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IAccountRepository
    {

        Guid SaveUpdateAccount(Account model);
        List<Account> GetAllAccounts();
        List<Account> GetAllUserAccounts(Guid userId);
        Account GetAccountById(Guid accountId);
        Guid DeleteAccount(Account model);
        Guid SaveUpdateAccountCreditTransaction(Account model);
        Guid SaveUpdateAccountDebitTransaction(List<Account> model);



    }
}
