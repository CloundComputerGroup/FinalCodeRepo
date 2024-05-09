using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class Resource
    {
        [Key]
        public Guid ResourcePk { get; set; }
        public Guid ResourceTypeFk { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CardNumber { get; set; }
        public string CheckbookNumber {get;set;}
        public Guid AccountFk { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModDate { get; set; }
        public int CardPIN { get; set; }
        [NotMapped]
        public AccountResourceType ResourceType { get; set; }
        [NotMapped]
        public Account ResourceAccount { get; set; }
        [NotMapped]
        public string ResourceTypeName { get; set; }
        [NotMapped]
        public string ActiveStatus { get; set; }
        [NotMapped]
        public string IssuedDate { get; set; }
        [NotMapped]
        public string ExpiryDateString { get; set; }
        [NotMapped]
        public int ConfirmOldPIN { get; set; }

    }
}
