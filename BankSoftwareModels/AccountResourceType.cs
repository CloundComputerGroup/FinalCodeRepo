using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class AccountResourceType
    {
        [Key]
        public Guid AccountResourceTypePk { get; set; }
        public string ResourceName { get; set; }
        public string Description { get; set; }
    }
}
