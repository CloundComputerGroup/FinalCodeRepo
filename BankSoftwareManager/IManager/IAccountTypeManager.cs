using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.IManager
{
    public interface IAccountTypeManager
    {
        Guid SaveUpdateAccountType(AccountType model);
        List<AccountType> GetAllAccountType();
        AccountType GetAccountTypeById(Guid accountTypeId);
    }
}
