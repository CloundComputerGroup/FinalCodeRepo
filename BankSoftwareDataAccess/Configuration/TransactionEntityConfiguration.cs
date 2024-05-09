using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class TransactionEntityConfiguration : EntityConfiguration<Transaction>
    {
        public TransactionEntityConfiguration()
        {
            ToTable("Transaction");
            HasKey(x => x.TransactionPk);
        }
    }
}
