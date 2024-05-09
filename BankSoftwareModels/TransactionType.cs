using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class TransactionType
    {
        [Key]
        public Guid TransactionTypePk { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<Transaction> Transactions { get; set; }
    }
}
