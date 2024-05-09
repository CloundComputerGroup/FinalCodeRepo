using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class PaymentDetailEntityConfiguration : EntityConfiguration<PaymentDetail>
    {
        public PaymentDetailEntityConfiguration()
        {
            ToTable("PaymentDetail");
            HasKey(x => x.PaymentPk);
        }
    }
}
