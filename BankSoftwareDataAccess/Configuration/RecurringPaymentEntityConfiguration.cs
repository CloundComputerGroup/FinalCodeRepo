using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class RecurringPaymentEntityConfiguration : EntityConfiguration<RecurringPayment>
    {
        public RecurringPaymentEntityConfiguration()
        {
            ToTable("RecurringPayment");
            HasKey(x => x.RecurringPaymentPk);
        }
    }
}
