using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface ITransactionTypeRepository
    {

        Guid SaveUpdateTransactionType(TransactionType model);
        List<TransactionType> GetAllTransactionType();
        TransactionType GetTransactionTypeById(Guid transactionTypeId);

       
    }
}
