using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IAccountTypeRepository
    {

        Guid SaveUpdateAccountType(AccountType model);
        List<AccountType> GetAllAccountType();
        AccountType GetAccountTypeById(Guid accountTypeId);

       
    }
}
