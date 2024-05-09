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
    public class RecurringPaymentRepository : EntityRepositoryBase<RecurringPayment>, IRecurringPaymentRepository
    {
        private IUnitOfWork unitOfWork;

        public RecurringPaymentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateRecurringPayment(RecurringPayment model)
        {
            var payment = DataContext.RecurringPayments.Count(u => u.RecurringPaymentPk == model.RecurringPaymentPk) > 0;
            if (!payment)
            {
                model.RecurringPaymentPk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.RecurringPaymentPk;
        }


        public List<RecurringPayment> GetAllRecurringPayment()
        {
            return DataContext.RecurringPayments.ToList();
        }
        
        
        public RecurringPayment GetRecurringPaymentById(Guid paymentId)
        {
            return DataContext.RecurringPayments.Where(x => x.RecurringPaymentPk == paymentId).FirstOrDefault();
        }
        


        
    }
}
