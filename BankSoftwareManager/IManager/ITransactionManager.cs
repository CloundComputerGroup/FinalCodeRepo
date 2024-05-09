using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.IManager
{
    public interface ITransactionManager
    {
        Guid SaveUpdateTransaction(Transaction model);
        List<Transaction> GetAllTransaction(Guid accountId);
        Transaction GetTransactionById(Guid transactionId);
    }
}
