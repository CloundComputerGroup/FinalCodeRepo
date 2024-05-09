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
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;
        public AccountManager(IAccountRepository accountRepository, IUserRepository userRepository, IBankRepository bankRepository, IAccountTypeRepository accountTypeRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _bankRepository = bankRepository;
            _accountTypeRepository = accountTypeRepository;
        }

        public Guid SaveUpdateAccount(Account model)
        {
            return _accountRepository.SaveUpdateAccount(model);
        }

        public Guid SaveUpdateAccountCreditTransaction(Account model)
        {
            return _accountRepository.SaveUpdateAccountCreditTransaction(model);
        }

        public Guid SaveUpdateAccountDebitTransaction(List<Account> model)
        {
            return _accountRepository.SaveUpdateAccountDebitTransaction(model);
        }

        public Guid DeleteAccount(Account model)
        {
            return _accountRepository.DeleteAccount(model);
        }
        public List<Account> GetAllAccount()
        {
            var accountDetail = _accountRepository.GetAllAccounts();
            foreach (var item in accountDetail)
            {
                var bankDetail = _bankRepository.GetBankById(item.BankFk);
                var userDetail = _userRepository.GetUserById(item.UserFk);
                var accountType = _accountTypeRepository.GetAccountTypeById(item.AccountTypeFk);
                item.BankName = bankDetail.BankName;
                item.UserName = userDetail.Name;
                item.AccountTypeName = accountType.Name;
            }
            
            
            return accountDetail;
        }

        public List<Account> GetAllUserAccounts(Guid userId)
        {
            var accountDetail = _accountRepository.GetAllUserAccounts(userId);
            foreach (var item in accountDetail)
            {
                var userDetail = _userRepository.GetUserById(item.UserFk);
                var accountType = _accountTypeRepository.GetAccountTypeById(item.AccountTypeFk);
                item.AccountName = item.AccountName;//item.AccountNumber + " __ " + item.AccountName + " (" + accountType.Name +")";
                item.UserName = userDetail.Name;
                item.AccountTypeName = accountType.Name;
            }


            return accountDetail;
        }

        public Account GetAccountById(Guid accountId)
        {
            return _accountRepository.GetAccountById(accountId);
        }
    }
}
