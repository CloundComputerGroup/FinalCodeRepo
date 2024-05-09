using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IPaymentDetailRepository
    {

        Guid SaveUpdatePaymentDetail(PaymentDetail model);
        List<PaymentDetail> GetAllPaymentDetail();
        PaymentDetail GetPaymentDetailById(Guid paymentId);
        List<PaymentDetail> GetAllPaymentDetailByRecId(Guid recPaymentId);
        Guid SaveUpdatePaymentDetailList(List<PaymentDetail> model);



    }
}
