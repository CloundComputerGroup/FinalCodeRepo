using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IRecurringPaymentRepository
    {

        Guid SaveUpdateRecurringPayment(RecurringPayment model);
        List<RecurringPayment> GetAllRecurringPayment();
        RecurringPayment GetRecurringPaymentById(Guid paymentId);

       
    }
}
