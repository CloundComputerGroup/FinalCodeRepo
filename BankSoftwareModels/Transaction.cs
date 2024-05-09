using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class Transaction
    {
        [Key]
        public Guid TransactionPk { get; set; }
        public Guid AccountFk { get; set; }
        public decimal TransAmount { get; set; }
        public DateTime TransDate { get; set; }
        public string ChequeNumber { get; set; }
        public string CardNumber { get; set;  }
        [NotMapped]
        public Account TransactionAccount { get; set; }
        public Guid TransactionTypeFk { get; set; }
        public Guid ReferenceAccountFk { get; set; }
        [NotMapped]
        public string TransactionType { get; set; }
        public string Reference { get; set; }
        public bool IsCreditTrans { get; set; }
        public bool IsDebitTrans { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [NotMapped]
        public string AccountNumber { get; set; }
        [NotMapped]
        public string TransactionTypeName { get; set; }
        [NotMapped]
        public string TransNature { get; set; }
        [NotMapped]
        public string TransDateFormated { get; set; }

    }
}
