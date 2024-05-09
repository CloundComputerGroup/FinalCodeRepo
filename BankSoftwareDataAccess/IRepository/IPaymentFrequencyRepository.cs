using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IPaymentFrequencyRepository
    {

        Guid SaveUpdatePaymentFrequency(PaymentFrequency model);
        List<PaymentFrequency> GetAllPaymentFrequency();
        PaymentFrequency GetPaymentFrequencyById(Guid frequencyId);

       
    }
}
