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
    public class BankManager : IBankManager
    {
        private readonly IBankRepository _bankRepository;
        public BankManager(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public Guid SaveUpdateBank(Bank model)
        {
            return _bankRepository.SaveUpdateBank(model);
        }
        public List<Bank> GetAllBanks()
        {
            return _bankRepository.GetAllBanks();
        }
        public Bank GetBankById(Guid bankId)
        {
            return _bankRepository.GetBankById(bankId);
        }
    }
}
