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
    public class AccountController : BaseController
    {
        private readonly IAccountManager _accountManager;
        private readonly ITransactionTypeManager _transactionTypeManager;
        public AccountController(ISessionManager sessionManager, IAccountManager accountManager, ITransactionTypeManager transactionTypeManager) : base(sessionManager)
        {
            this._accountManager = accountManager;
            this._transactionTypeManager = transactionTypeManager;
        }
        // GET: Account Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEdit(Guid? accountId)
        {
            if (accountId != null && accountId != Guid.Empty)
            {
                ViewBag.AccountId = accountId;
                var result = _accountManager.GetAccountById((Guid)accountId);
            }
            else {
                ViewBag.AccountId = Guid.Empty;
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAccount(Guid accountId)
        {
            try
            {
                var result = Guid.Empty;
                if (accountId != Guid.Empty && accountId!=null)
                {
                    var accountDetail = _accountManager.GetAccountById(accountId);
                    accountDetail.IsActive = false;
                    accountDetail.IsDeleted = true;
                    accountDetail.UserFk = sessionManager.LoggedInUser.UserPk;
                    result = _accountManager.DeleteAccount(accountDetail);

                }
                return View("Index");
            }
            catch (Exception e)
            {
                throw;
            }


        }

        public ActionResult ViewDetail(Guid? accountId)
        {
            if (accountId != null && accountId != Guid.Empty)
            {
                ViewBag.AccountId = accountId;
                var result = _accountManager.GetAccountById((Guid)accountId);
            }
            else
            {
                ViewBag.AccountId = Guid.Empty;
            }
            return View();
        }


        public ActionResult GetAllAccount()
        {
            try 
            {
                var result = _accountManager.GetAllAccount();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetAllUserAccount()
        {
            try
            {
                var userId = sessionManager.LoggedInUser.UserPk;
                var result = _accountManager.GetAllUserAccounts(userId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetAllBeneficiaryAccount()
        {
            try
            {
                var userId = sessionManager.LoggedInUser.UserPk;
                var result = _accountManager.GetAllAccount().Where(x=>x.UserFk!=userId).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetAllTransferBeneficiaryAccount(Guid selectedAccountId)
        {
            try
            {
                var userId = sessionManager.LoggedInUser.UserPk;
                var result = _accountManager.GetAllAccount().Where(x => x.AccountPk != selectedAccountId).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetAllTransactionTypes()
        {
            try
            {
                var result = _transactionTypeManager.GetAllTransactionType();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetAccountById(Guid accountId)
        {
            try
            {
                var result = _accountManager.GetAccountById(accountId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateAccount(Account model)
        {
            try
            {
                if (model.AccountPk == Guid.Empty)
                {
                    model.CreateDate = DateTime.Now;
                    model.IsActive = false;
                    model.UserFk = sessionManager.LoggedInUser.UserPk;
                    var bankDetail = Session["BankDetail"] as Bank;
                    model.BankFk = bankDetail.BankPk;
                    String startWith = "16";
                    Random generator = new Random();
                    String r = generator.Next(0, 999999).ToString("D6");
                    String aAccounNumber = startWith + r;
                    model.AccountNumber = aAccounNumber;

                }
                else {
                    var account = _accountManager.GetAccountById(model.AccountPk);
                    account.SecurityPIN = model.SecurityPIN;
                    account.Balance = model.Balance;
                    account.AccountTypeFk = model.AccountTypeFk;
                    model = account;
                }
                model.IsDeleted = false;
                model.ModDate = DateTime.Now;
                var result = _accountManager.SaveUpdateAccount(model);
                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}