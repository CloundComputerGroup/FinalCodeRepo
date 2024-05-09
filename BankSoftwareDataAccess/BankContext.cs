using BankSoftwareDataAccess.Configuration;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess
{
    public class BankContext: DbContext, IBankContext
    {
        public BankContext() : base("name=DefaultDbConnection")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Bank> Banks { get; set; }
        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<AccountResourceType> AccountResourceTypes { get; set; }
        public IDbSet<AccountType> AccountTypes { get; set; }
        public IDbSet<Resource> Resources { get; set; }
        public IDbSet<Transaction> Transactions { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<TransactionType> TransactionTypes { get; set; }
        public IDbSet<RecurringPayment> RecurringPayments { get; set; }
        public IDbSet<PaymentFrequency> PaymentFrequency { get; set; }
        public IDbSet<PaymentDetail> PaymentDetail { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new BankEntityConfiguration());
            modelBuilder.Configurations.Add(new AccountEntityConfiguration());
            modelBuilder.Configurations.Add(new AccountTypeEntityConfiguration());
            modelBuilder.Configurations.Add(new AccountResourceTypeEntityConfiguration());
            modelBuilder.Configurations.Add(new ResourceEntityConfiguration());
            modelBuilder.Configurations.Add(new TransactionEntityConfiguration());
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new TransactionTypeEntityConfiguration());
            modelBuilder.Configurations.Add(new RecurringPaymentEntityConfiguration());
            modelBuilder.Configurations.Add(new PaymentFrequencyEntityConfiguration());
            modelBuilder.Configurations.Add(new PaymentDetailEntityConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
