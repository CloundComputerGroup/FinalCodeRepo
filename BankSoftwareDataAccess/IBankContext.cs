using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess
{
    public interface IBankContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        IDbSet<Bank> Banks { get; set; }
        IDbSet<Account> Accounts { get; set; }
        IDbSet<AccountResourceType> AccountResourceTypes { get; set; }
        IDbSet<AccountType> AccountTypes { get; set; }
        IDbSet<Resource> Resources { get; set; }
        IDbSet<Transaction> Transactions { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<TransactionType> TransactionTypes { get; set; }
        IDbSet<RecurringPayment> RecurringPayments { get; set; }
        IDbSet<PaymentFrequency> PaymentFrequency { get; set; }
        IDbSet<PaymentDetail> PaymentDetail { get; set; }

        int SaveChanges();

    }
}
