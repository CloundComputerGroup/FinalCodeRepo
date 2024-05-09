using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class RecurringPayment
    {
        [Key]
        public Guid RecurringPaymentPk { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid FrequencyFk { get; set; }
        public int NumberOfPayment { get; set; }
        public Guid CustomerAccountFk { get; set; }
        public DateTime StartDate { get; set; }
        public Guid ReceivableAccountFk { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime CreateDate { get; set; }
        [NotMapped]
        public string StartDateString { get; set; }
        [NotMapped]
        public string CustomerAccountName { get; set; }
        [NotMapped]
        public string RecievingAccountName { get; set; }
        [NotMapped]
        public string FrequencyString { get; set; }
    }
}
