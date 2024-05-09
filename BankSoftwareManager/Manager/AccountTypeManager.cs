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
    public class AccountTypeManager : IAccountTypeManager
    {
        private readonly IAccountTypeRepository _accountTypeRepository;
        public AccountTypeManager(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }

        public Guid SaveUpdateAccountType(AccountType model)
        {
            return _accountTypeRepository.SaveUpdateAccountType(model);
        }
        public List<AccountType> GetAllAccountType()
        {
            return _accountTypeRepository.GetAllAccountType();
        }
        public AccountType GetAccountTypeById(Guid accountId)
        {
            return _accountTypeRepository.GetAccountTypeById(accountId);
        }
    }
}
