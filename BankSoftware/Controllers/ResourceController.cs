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
    public class ResourceController : BaseController
    {
        private readonly IResourceManager _resourceManager;
        private readonly IAccountResourceTypeManager _accountResourceTypeManager;
        private readonly ISessionManager _sessionManager;
        public ResourceController(ISessionManager sessionManager, IResourceManager resourceManager, IAccountResourceTypeManager accountResourceTypeManager) : base(sessionManager)
        {
            this._resourceManager = resourceManager;
            this._accountResourceTypeManager = accountResourceTypeManager;
            this._sessionManager = sessionManager;

        }
        // GET: Account Login
        public ActionResult CardIndex()
        {
            return View();
        }
        public ActionResult Chequebook()
        {
            return View();
        }

        public ActionResult GetAllResource()
        {
            try 
            {
                var result = _resourceManager.GetAllResource();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetAllChequebookResource()
        {
            try
            {
                var userId = _sessionManager.CurrentUser.UserPk;
                var result = _resourceManager.GetAllUserChequebooks(userId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetAllUserCards()
        {
            try
            {
                var userId = _sessionManager.CurrentUser.UserPk;
                var result = _resourceManager.GetAllUserCards(userId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetCardsResourcesType()
        {
            try
            {
                var resourceTypes = _accountResourceTypeManager.GetAllAccountResourceType().Where(x => x.ResourceName == "Debit Card" || x.ResourceName == "Credit Card").ToList();
                return Json(resourceTypes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetResourceById(Guid resourceId)
        {
            try
            {
                var result = _resourceManager.GetResourceById(resourceId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateResource(Resource model)
        {
            var result = Guid.Empty;
            try
            {
                if (model.AccountFk != Guid.Empty && model.AccountFk != null && (model.ResourcePk==Guid.Empty || model.ResourcePk==null))
                {
                    var resourceTypes = _accountResourceTypeManager.GetAllAccountResourceType().Where(x=>x.ResourceName == "Chequebook").FirstOrDefault();
                    model.ResourceTypeFk = resourceTypes != null ? resourceTypes.AccountResourceTypePk : Guid.Empty;
                    model.IsActive = true;
                    model.CreateDate = DateTime.Now;
                    model.ExpiryDate = DateTime.Now;
                    model.ModDate = DateTime.Now;
                    result = _resourceManager.SaveUpdateResource(model);
                }
                else if ((model.ResourcePk != Guid.Empty && model.ResourcePk != null))
                {
                    model.IsActive = false;
                    model.ModDate = DateTime.Now;
                    result = _resourceManager.SaveUpdateResource(model);
                }
                
                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
               return new JsonResult() { Data = result };
            }

        }

        //ChangeCardPIN
        [HttpPost]
        public ActionResult ChangeCardPIN(Resource model)
        {
            var result = Guid.Empty;
            try
            {
                if ((model.ResourcePk != Guid.Empty && model.ResourcePk != null))
                {
                    var resource = _resourceManager.GetResourceById(model.ResourcePk);
                    if (resource != null && resource.CardPIN == model.ConfirmOldPIN)
                    {
                        resource.CardPIN = model.CardPIN;
                        resource.ModDate = DateTime.Now;
                        result = _resourceManager.SaveUpdateResource(resource);
                    }

                }

                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = result };
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateCards(Resource model)
        {
            var result = Guid.Empty;
            try
            {
                if (model.AccountFk != Guid.Empty && model.AccountFk != null && (model.ResourcePk == Guid.Empty || model.ResourcePk == null))
                {
                   
                    model.IsActive = true;
                    model.CreateDate = DateTime.Now;
                    model.ExpiryDate = DateTime.Now.AddYears(5);
                    model.ModDate = DateTime.Now;
                    Random rnd = new Random();
                    string cardNumber = string.Empty;

                    for (int i = 0; i < 16; i++)
                    {
                        cardNumber += rnd.Next(0, 9).ToString();
                    }
                    model.CardNumber = cardNumber;
                    result = _resourceManager.SaveUpdateResource(model);
                }
                else if ((model.ResourcePk != Guid.Empty && model.ResourcePk != null))
                {
                    var resource = _resourceManager.GetResourceById(model.ResourcePk);
                    resource.IsActive = false;
                    resource.ModDate = DateTime.Now;
                    result = _resourceManager.SaveUpdateResource(resource);
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