using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class BankEntityConfiguration : EntityConfiguration<Bank>
    {
        public BankEntityConfiguration()
        {
            ToTable("Bank");
            HasKey(x => x.BankPk);
        }
    }
}
