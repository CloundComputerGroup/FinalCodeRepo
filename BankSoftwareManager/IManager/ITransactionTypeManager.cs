using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.IManager
{
    public interface ITransactionTypeManager
    {
        Guid SaveUpdateTransactionType(TransactionType model);
        List<TransactionType> GetAllTransactionType();
        TransactionType GetTransactionTypeById(Guid transactionTypeId);
    }
}
