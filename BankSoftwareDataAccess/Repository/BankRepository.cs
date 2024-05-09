using BankSoftwareDataAccess.EntityRepository;
using BankSoftwareDataAccess.IRepository;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Repository
{
    public class BankRepository : EntityRepositoryBase<Bank>, IBankRepository
    {
        private IUnitOfWork unitOfWork;

        public BankRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateBank(Bank model)
        {
            var bank = DataContext.Banks.Count(u => u.BankPk == model.BankPk) > 0;
            if (!bank)
            {
                model.BankPk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.BankPk;
        }


        public List<Bank> GetAllBanks()
        {
            return DataContext.Banks.ToList();
        }
        
        
        public Bank GetBankById(Guid bankId)
        {
            return DataContext.Banks.Where(x => x.BankPk == bankId).FirstOrDefault();
        }
        


        
    }
}
