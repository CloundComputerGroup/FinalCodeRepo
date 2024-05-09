using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class AccountTypeEntityConfiguration : EntityConfiguration<AccountType>
    {
        public AccountTypeEntityConfiguration()
        {
            ToTable("AccountType");
            HasKey(x => x.AccountTypePk);
        }
    }
}
