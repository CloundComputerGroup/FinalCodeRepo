using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.IManager
{
    public interface IAccountManager
    {
        Guid SaveUpdateAccount(Account model);
        List<Account> GetAllAccount();
        List<Account> GetAllUserAccounts(Guid userId);
        Guid SaveUpdateAccountCreditTransaction(Account model);
        Account GetAccountById(Guid accountId);
        Guid DeleteAccount(Account model);
        Guid SaveUpdateAccountDebitTransaction(List<Account> model);
    }
}
