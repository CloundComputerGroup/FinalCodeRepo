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
    public class BankController : BaseController
    {
        private readonly IBankManager _bankManager;
        public BankController(ISessionManager sessionManager, IBankManager bankManager) : base(sessionManager)
        {
            this._bankManager = bankManager;
        }
        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBankById()
        {
            try 
            {
                var result = _bankManager.GetAllBanks();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveBank(Bank model)
        {
            try
            {
                var result = _bankManager.SaveUpdateBank(model);
                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}