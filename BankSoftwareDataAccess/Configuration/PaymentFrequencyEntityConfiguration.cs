using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class PaymentFrequencyEntityConfiguration : EntityConfiguration<PaymentFrequency>
    {
        public PaymentFrequencyEntityConfiguration()
        {
            ToTable("PaymentFrequency");
            HasKey(x => x.FrequencyPk);
        }
    }
}
