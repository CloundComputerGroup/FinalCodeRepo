using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class AccountEntityConfiguration : EntityConfiguration<Account>
    {
        public AccountEntityConfiguration()
        {
            ToTable("Account");
            HasKey(x => x.AccountPk);
        }
    }
}
