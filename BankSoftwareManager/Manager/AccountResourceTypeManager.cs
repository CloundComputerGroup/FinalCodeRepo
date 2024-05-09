using BankSoftwareDataAccess.IRepository;
using BankSoftwareDataAccess.Repository;
using BankSoftwareManager.IManager;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.Manager
{
    public class AccountResourceTypeManager : IAccountResourceTypeManager
    {
        private readonly IAccountResourceTypeRepository _accountResourceTypeRepository;
        public AccountResourceTypeManager(IAccountResourceTypeRepository accountResourceTypeRepository)
        {
            _accountResourceTypeRepository = accountResourceTypeRepository;
        }
                    
        public Guid SaveUpdateAccountResourceType(AccountResourceType model)
        {
            return _accountResourceTypeRepository.SaveUpdateAccountResourceType(model);
        }
        public List<AccountResourceType> GetAllAccountResourceType()
        {
            return _accountResourceTypeRepository.GetAllAccountResourceType();
        }
        public AccountResourceType GetAccountResourceTypeById(Guid accountId)
        {
            return _accountResourceTypeRepository.GetAccountResourceTypeById(accountId);
        }
    }
}
