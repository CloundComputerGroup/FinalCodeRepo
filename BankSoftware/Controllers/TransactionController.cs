using BankSoftware.Utilities;
using BankSoftwareManager.IManager;
using BankSoftwareManager.Manager;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankSoftware.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionManager _transactionManager;
        private readonly IAccountManager _accountManager;
        public TransactionController(ISessionManager sessionManager, ITransactionManager transactionManager, IAccountManager accountManager) : base(sessionManager)
        {
            this._transactionManager = transactionManager;
            this._accountManager = accountManager;
        }
        // GET: Account Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllTransaction(Guid accountId)
        {
            try 
            {
                var result = _transactionManager.GetAllTransaction(accountId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetTransactionById(Guid transactionId)
        {
            try
            {
                var result = _transactionManager.GetTransactionById(transactionId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateTransaction(Transaction model)
        {
            var result = Guid.Empty;
            try
            {
                Account accountDetail = new Account();
                accountDetail.AccountPk = model.AccountFk;
                accountDetail.Balance = model.TransAmount;

                var accountResult = _accountManager.SaveUpdateAccountCreditTransaction(accountDetail);
                if (accountResult != Guid.Empty)
                {
                    model.TransDate = DateTime.Now;
                    if (model.TransAmount < 0)
                    {
                        model.IsCreditTrans = false;
                        model.IsDebitTrans = true;
                    }
                    else {
                        model.IsCreditTrans = true;
                        model.IsDebitTrans = false;
                    }
                       
                    result = _transactionManager.SaveUpdateTransaction(model);
                }

                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = result };
            }

        }

        [HttpPost]
        public ActionResult SaveUpdatePaymentTransaction(Transaction model)
        {
            var result = Guid.Empty;
            try
            {

                Account creditAccountDetail = new Account();
                Account debitAccountDetail = new Account();
                creditAccountDetail.AccountPk = model.ReferenceAccountFk;
                creditAccountDetail.Balance = model.TransAmount;
                debitAccountDetail.AccountPk = model.AccountFk;
                debitAccountDetail.Balance = -1*model.TransAmount;
                List<Account> accountsList = new List<Account>();
                accountsList.Add(creditAccountDetail);
                accountsList.Add(debitAccountDetail);

                //var accountResult = _accountManager.SaveUpdateAccountCreditTransaction(creditAccountDetail);
                var debitAccountResult = _accountManager.SaveUpdateAccountDebitTransaction(accountsList);
                if (debitAccountResult != Guid.Empty)
                {
                    Transaction newTransaction;
                    foreach (var item in accountsList)
                    {
                        newTransaction = new Transaction();
                        if (item.Balance < 0)
                        {

                            newTransaction.AccountFk = item.AccountPk;
                            newTransaction.ReferenceAccountFk = model.ReferenceAccountFk;
                            newTransaction.Reference = model.Reference;
                            newTransaction.TransAmount = item.Balance;
                            newTransaction.TransDate = DateTime.Now;
                            newTransaction.TransactionTypeFk = model.TransactionTypeFk;
                            newTransaction.IsCreditTrans = false;
                            newTransaction.IsDebitTrans = true;

                        }
                        else {
                            newTransaction.AccountFk = item.AccountPk;
                            newTransaction.ReferenceAccountFk = model.AccountFk;
                            newTransaction.Reference = model.Reference;
                            newTransaction.TransAmount = item.Balance;
                            newTransaction.TransDate = DateTime.Now;
                            newTransaction.TransactionTypeFk = model.TransactionTypeFk;
                            newTransaction.IsCreditTrans = true;
                            newTransaction.IsDebitTrans = false;
                        }
                        result = _transactionManager.SaveUpdateTransaction(newTransaction);

                    }
                    
                }

                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = result };
            }

        }
    }
}