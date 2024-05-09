using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IBankRepository
    {

        Guid SaveUpdateBank(Bank model);
        List<Bank> GetAllBanks();
        Bank GetBankById(Guid bankId);

       
    }
}
