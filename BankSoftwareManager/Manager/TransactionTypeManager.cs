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
    public class TransactionTypeManager : ITransactionTypeManager
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        public TransactionTypeManager(ITransactionTypeRepository transactionTypeRepository)
        {
            _transactionTypeRepository = transactionTypeRepository;
        }

        public Guid SaveUpdateTransactionType(TransactionType model)
        {
            return _transactionTypeRepository.SaveUpdateTransactionType(model);
        }
        public List<TransactionType> GetAllTransactionType()
        {
            return _transactionTypeRepository.GetAllTransactionType();
        }
        public TransactionType GetTransactionTypeById(Guid transactionId)
        {
            return _transactionTypeRepository.GetTransactionTypeById(transactionId);
        }
    }
}
