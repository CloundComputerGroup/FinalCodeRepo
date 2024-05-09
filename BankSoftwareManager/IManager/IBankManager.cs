using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.IManager
{
    public interface IBankManager
    {
        Guid SaveUpdateBank(Bank model);
        List<Bank> GetAllBanks();
        Bank GetBankById(Guid bankId);
    }
}
