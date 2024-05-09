using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IAccountResourceTypeRepository
    {

        Guid SaveUpdateAccountResourceType(AccountResourceType model);
        List<AccountResourceType> GetAllAccountResourceType();
        AccountResourceType GetAccountResourceTypeById(Guid accountResourceTypeId);

       
    }
}
