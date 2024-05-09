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
    public class AccountTypeController : BaseController
    {
        private readonly IAccountTypeManager _accountTypeManager;
        public AccountTypeController(ISessionManager sessionManager, IAccountTypeManager accountTypeManager) : base(sessionManager)
        {
            this._accountTypeManager = accountTypeManager;
        }
        // GET: Account Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult GetAllAccountType()
        {
            try 
            {
                var result = _accountTypeManager.GetAllAccountType();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetAccountTypeById(Guid accountTypeId)
        {
            try
            {
                var result = _accountTypeManager.GetAccountTypeById(accountTypeId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateAccountType(AccountType model)
        {
            try
            {
                var result = _accountTypeManager.SaveUpdateAccountType(model);
                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}