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
    public class PaymentFrequencyRepository : EntityRepositoryBase<PaymentFrequency>, IPaymentFrequencyRepository
    {
        private IUnitOfWork unitOfWork;

        public PaymentFrequencyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdatePaymentFrequency(PaymentFrequency model)
        {
            var account = DataContext.PaymentFrequency.Count(u => u.FrequencyPk == model.FrequencyPk) > 0;
            if (!account)
            {
                model.FrequencyPk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.FrequencyPk;
        }


        public List<PaymentFrequency> GetAllPaymentFrequency()
        {
            return DataContext.PaymentFrequency.ToList();
        }
        
        
        public PaymentFrequency GetPaymentFrequencyById(Guid frequencyId)
        {
            return DataContext.PaymentFrequency.Where(x => x.FrequencyPk == frequencyId).FirstOrDefault();
        }
        


        
    }
}
