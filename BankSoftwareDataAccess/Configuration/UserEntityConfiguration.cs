using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class UserEntityConfiguration : EntityConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("User");
            HasKey(x => x.UserPk);
        }
    }
}
