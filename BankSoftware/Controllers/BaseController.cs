using BankSoftware.Utilities;
using BankSoftwareManager.IManager;
using BankSoftwareManager.Manager;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BankSoftware.Controllers
{
    public class BaseController : Controller
    {
        protected ISessionManager sessionManager;
        public BaseController(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var user = sessionManager.CurrentUser;
            if (user == null)
            {

                if (filterContext.Controller is UserController == false)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "User",
                        action = "Login"
                    }));
                }
            }

        }

        protected override void OnException(ExceptionContext filterContext)
        {
           // Logger.Error(string.Format("{0}. \r\n {1}", filterContext.Exception.Message, filterContext.Exception.StackTrace));
            base.OnException(filterContext);
        }
    }
}