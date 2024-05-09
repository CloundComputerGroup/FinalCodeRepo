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
    public interface IReccuringPaymentManager
    {
        Guid SaveUpdateRecurringPayment(RecurringPayment model);
        List<RecurringPayment> GetAllRecurringPayment();
        RecurringPayment GetRecurringPaymentById(Guid paymentId);

        //Frequency
        Guid SaveUpdatePaymentFrequency(PaymentFrequency model);
        List<PaymentFrequency> GetAllPaymentFrequency();
        PaymentFrequency GetPaymentFrequencyById(Guid frequencyId);

        //Payment Detail
        Guid SaveUpdatePaymentDetail(PaymentDetail model);
        List<PaymentDetail> GetAllPaymentDetail();
        PaymentDetail GetPaymentDetailById(Guid paymentId);
        List<PaymentDetail> GetAllPaymentDetailByRecId(Guid recPaymentId);
        Guid SaveUpdatePaymentDetailList(List<PaymentDetail> model);


    }
}
