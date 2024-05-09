using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class TransactionTypeEntityConfiguration : EntityConfiguration<TransactionType>
    {
        public TransactionTypeEntityConfiguration()
        {
            ToTable("TransactionType");
            HasKey(x => x.TransactionTypePk);
        }
    }
}
