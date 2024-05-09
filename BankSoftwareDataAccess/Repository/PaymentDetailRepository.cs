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
    public class PaymentDetailRepository : EntityRepositoryBase<PaymentDetail>, IPaymentDetailRepository
    {
        private IUnitOfWork unitOfWork;

        public PaymentDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdatePaymentDetail(PaymentDetail model)
        {
            var payment = DataContext.PaymentDetail.Count(u => u.PaymentPk == model.PaymentPk) > 0;
            if (!payment)
            {
                model.PaymentPk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.PaymentPk;
        }

        public Guid SaveUpdatePaymentDetailList(List<PaymentDetail> model)
        {
            foreach (var item in model)
            {
                item.PaymentPk = Guid.NewGuid();
                SetAdded(item);
                unitOfWork.SaveChanges();

            }
           
            return Guid.NewGuid();
        }


        public List<PaymentDetail> GetAllPaymentDetail()
        {
            return DataContext.PaymentDetail.ToList();
        }

        public List<PaymentDetail> GetAllPaymentDetailByRecId(Guid recPaymentId)
        {
            return DataContext.PaymentDetail.Where(x => x.RecurringPaymentFk == recPaymentId).ToList();
        }


        public PaymentDetail GetPaymentDetailById(Guid paymentId)
        {
            return DataContext.PaymentDetail.Where(x => x.PaymentPk == paymentId).FirstOrDefault();
        }
        


        
    }
}
