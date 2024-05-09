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
    public class AdminController : BaseController
    {
        private readonly IAccountTypeManager _accountTypeManager;
        public AdminController(ISessionManager sessionManager, IAccountTypeManager accountTypeManager) : base(sessionManager)
        {
            this._accountTypeManager = accountTypeManager;
        }
        // GET: Account Login
        public ActionResult AccountTypeIndex()
        {
            return View();
        }

        public ActionResult AddEdit()
        {
            return View();
        }

        public ActionResult GetAccountTypeAll()
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
        public ActionResult GetAccountById(Guid accountId)
        {
            try
            {
                var result = _accountTypeManager.GetAccountTypeById(accountId);
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