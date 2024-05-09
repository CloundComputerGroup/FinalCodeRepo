using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class PaymentFrequency
    {
        [Key]
        public Guid FrequencyPk { get; set; }
        public string Name { get; set; }
        
        //[NotMapped]
    }
}
