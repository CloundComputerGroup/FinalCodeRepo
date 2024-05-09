using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class User
    {
        [Key]
        public Guid UserPk { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public DateTime DOB { get; set; }
        public string NIC { get; set; }
        public string Gender { get; set; }
        public Guid? ResetPasswordId { get; set; }
        public string Secret { get; set; }
        [NotMapped]
        public List<Account> Accounts { get; set; }
    }
}
