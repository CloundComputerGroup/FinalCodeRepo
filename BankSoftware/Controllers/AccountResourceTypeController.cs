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
    public class AccountResourceTypeController : BaseController
    {
        private readonly IAccountResourceTypeManager _accountResourceTypeManager;
        public AccountResourceTypeController(IAccountResourceTypeManager accountResourceTypeManager, ISessionManager sessionManager) : base(sessionManager)
        {
            this._accountResourceTypeManager = accountResourceTypeManager;
        }
        // GET: AccountResourceType
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult GetAllAccountResourceType()
        {
            try 
            {
                var result = _accountResourceTypeManager.GetAllAccountResourceType();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetAccountResourceTypeById(Guid accountResourceTypeId)
        {
            try
            {
                var result = _accountResourceTypeManager.GetAccountResourceTypeById(accountResourceTypeId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateAccountResourceType(AccountResourceType model)
        {
            try
            {
                var result = _accountResourceTypeManager.SaveUpdateAccountResourceType(model);
                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}