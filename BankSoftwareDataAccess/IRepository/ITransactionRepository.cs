using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface ITransactionRepository
    {

        Guid SaveUpdateTransaction(Transaction model);
        List<Transaction> GetAllTransaction(Guid accountId);
        Transaction GetTransactionById(Guid transactionId);

       
    }
}
