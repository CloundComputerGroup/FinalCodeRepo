using BankSoftware.Utilities;
using BankSoftwareDataAccess;
using BankSoftwareDataAccess.IRepository;
using BankSoftwareDataAccess.Repository;
using BankSoftwareManager.IManager;
using BankSoftwareManager.Manager;
using System;

using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace BankSoftware
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IUnitOfWork, UnitOfWork>()
                     .RegisterType<IBankContext, BankContext>()
                     .RegisterType<IAccountRepository, AccountRepository>()
                     .RegisterType<IAccountTypeRepository, AccountTypeRepository>()
                     .RegisterType<IAccountResourceTypeRepository, AccountResourceTypeRepository>()
                     .RegisterType<IBankRepository, BankRepository>()
                     .RegisterType<IResourceRepository, ResourceRepository>()
                     .RegisterType<ITransactionRepository, TransactionRepository>()
                     .RegisterType<IUserRepository, UserRepository>()
                     .RegisterType<IAccountManager, AccountManager>()
                     .RegisterType<IAccountTypeManager, AccountTypeManager>()
                     .RegisterType<IAccountResourceTypeManager, AccountResourceTypeManager>()
                     .RegisterType<IBankManager, BankManager>()
                     .RegisterType<IResourceManager, ResourceManager>()
                     .RegisterType<ITransactionManager, TransactionManager>()
                     .RegisterType<ITransactionTypeRepository, TransactionTypeRepository>()
                     .RegisterType<ITransactionTypeManager, TransactionTypeManager>()
                     .RegisterType<IRecurringPaymentRepository, RecurringPaymentRepository>()
                     .RegisterType<IPaymentFrequencyRepository, PaymentFrequencyRepository>()
                     .RegisterType<IPaymentDetailRepository, PaymentDetailRepository>()
                     .RegisterType<IReccuringPaymentManager, ReccuringPaymentManager>()



                     .RegisterType<IUserManager, UserManager>();
            //.RegisterType<IBPPDbContext, BPPDbContext>();
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<ISessionManager>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => new SessionManager()));


        }
    }
}