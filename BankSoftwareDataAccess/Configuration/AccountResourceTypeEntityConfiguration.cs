using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class AccountResourceTypeEntityConfiguration : EntityConfiguration<AccountResourceType>
    {
        public AccountResourceTypeEntityConfiguration()
        {
            ToTable("AccountResourceType");
            HasKey(x => x.AccountResourceTypePk);
        }
    }
}
