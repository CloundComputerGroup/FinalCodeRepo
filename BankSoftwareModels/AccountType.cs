using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class AccountType
    {
        [Key]
        public Guid AccountTypePk { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public List<Account> Accounts { get; set; }
    }
}
