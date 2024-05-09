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
    public class TransactionManager : ITransactionManager
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IAccountRepository _accountRepository;
        public TransactionManager(ITransactionRepository transactionRepository, ITransactionTypeRepository transactionTypeRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _accountRepository = accountRepository;
        }

        public Guid SaveUpdateTransaction(Transaction model)
        {
            return _transactionRepository.SaveUpdateTransaction(model);
        }
        public List<Transaction> GetAllTransaction(Guid accountId)
        {
            var transactions =  _transactionRepository.GetAllTransaction(accountId);
            foreach (var item in transactions)
            {
                var account = _accountRepository.GetAccountById(item.AccountFk);
                var transactionType = _transactionTypeRepository.GetTransactionTypeById(item.TransactionTypeFk);
                item.AccountName = account.AccountName;
                item.AccountNumber = account.AccountNumber;
                item.TransactionTypeName = transactionType.Name;
                item.TransNature = item.IsCreditTrans == true ? "Credit" : "Debit";
                item.TransDateFormated = item.TransDate.ToString("MM/dd/yyyy HH:mm");

            }
            return transactions;
        }
        public Transaction GetTransactionById(Guid transactionId)
        {
            return _transactionRepository.GetTransactionById(transactionId);
        }
    }
}
