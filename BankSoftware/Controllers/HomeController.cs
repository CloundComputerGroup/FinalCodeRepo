using BankSoftware.Utilities;
using BankSoftwareManager.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankSoftware.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly ITransactionManager _transactionManager;
        private readonly IAccountManager _accountManager;
        public HomeController(ISessionManager sessionManager, IUserManager userManager, ITransactionManager transactionManager, IAccountManager accountManager) : base(sessionManager)
        {
            this._userManager = userManager;
            this._transactionManager = transactionManager;
            this._accountManager = accountManager;
        }
        public ActionResult Index()
        {
            var userId = sessionManager.LoggedInUser.UserPk;
            var userAccounts = _accountManager.GetAllUserAccounts(userId);
            decimal balance = 0;
            decimal businessBalance = 0;
            decimal personalBalance = 0;
            decimal creditTrans = 0;
            decimal debitTrans = 0;
            foreach (var item in userAccounts)
            {
                balance = balance + item.Balance;
                if (item.AccountTypeName == "Business")
                {
                    businessBalance = businessBalance + item.Balance;
                }
                else {
                    personalBalance = personalBalance + item.Balance;
                }

                var userTransactions = _transactionManager.GetAllTransaction(item.AccountPk);
                foreach (var trans in userTransactions)
                {
                    if (trans.TransNature == "Credit")
                    {
                        creditTrans = creditTrans + trans.TransAmount;
                    }
                    if (trans.TransNature == "Debit")
                    {
                        debitTrans = debitTrans + trans.TransAmount;
                    }
                }
            }
            ViewBag.TotalAmount = balance;
            ViewBag.BusinessBalance = businessBalance;
            ViewBag.PersonalBalance = personalBalance;
            ViewBag.NumberOfAccount = userAccounts.Count();
            //TransNature
           
           
            ViewBag.CreditAmount = creditTrans;
            ViewBag.DebitAmount = debitTrans;
            ViewBag.UserName = sessionManager.LoggedInUser.Name;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}