using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class Account
    {
        [Key]
        public Guid AccountPk { get; set; }
        public string AccountName { get; set; }
        public Guid AccountTypeFk { get; set; }
        [NotMapped]
        public string AccountTypeName { get; set; }
        public string AccountNumber { get; set; }
        public Guid UserFk { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        public Guid BankFk { get; set; }
        [NotMapped]
        public string BankName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModDate { get; set; }
        public bool IsActive { get; set; }
        public int SecurityPIN { get; set; }
        [NotMapped]
        public List<Resource> Resources { get; set; }
        [NotMapped]
        public User AccountUser { get; set; }
        [NotMapped]
        public Bank AccoutnBank { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
    }
}
