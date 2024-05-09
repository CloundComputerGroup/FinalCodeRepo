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
    public class RecurringPaymentController : BaseController
    {
        private readonly IReccuringPaymentManager _recurringPaymentManager;
        public RecurringPaymentController(ISessionManager sessionManager, IReccuringPaymentManager accountTypeManager) : base(sessionManager)
        {
            this._recurringPaymentManager = accountTypeManager;
        }
        // GET: Account Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllRecurringPayment()
        {
            try 
            {
                var result = _recurringPaymentManager.GetAllRecurringPayment();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult GetAllPaymentFrequency()
        {
            try
            {
                var result = _recurringPaymentManager.GetAllPaymentFrequency();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetRecurringPaymentById(Guid paymentId)
        {
            try
            {
                var result = _recurringPaymentManager.GetRecurringPaymentById(paymentId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateAccountType(RecurringPayment model)
        {
            try
            {
                var result = _recurringPaymentManager.SaveUpdateRecurringPayment(model);
                List<PaymentDetail> paymentDetailList = new List<PaymentDetail>();
                PaymentDetail paymentDetail = new PaymentDetail();
                int numberofpayments = model.NumberOfPayment;
                var frequency = _recurringPaymentManager.GetPaymentFrequencyById(model.FrequencyFk);
                if (frequency.Name == "Daily")
                {
                    DateTime startDate = model.StartDate;
                    for (int i = 0; i < numberofpayments; i++)
                    {
                        paymentDetail.PaymentDate = startDate;
                        paymentDetail.PaymentAmount = model.PaymentAmount;
                        paymentDetail.RecurringPaymentFk = result;
                        paymentDetail.Description = model.Description;
                        paymentDetailList.Add(paymentDetail);
                        startDate = startDate.AddDays(1);
                    }

                }
                else if (frequency.Name == "Weekly")
                {
                    DateTime startDate = model.StartDate;
                    for (int i = 0; i < numberofpayments; i++)
                    {
                        paymentDetail = new PaymentDetail();
                        paymentDetail.PaymentDate = startDate;
                        paymentDetail.PaymentAmount = model.PaymentAmount;
                        paymentDetail.RecurringPaymentFk = result;
                        paymentDetail.Description = model.Description;
                        paymentDetailList.Add(paymentDetail);
                        startDate = startDate.AddDays(7);
                    }

                }
                else if (frequency.Name == "Monthly")
                {
                    DateTime startDate = model.StartDate;
                    for (int i = 0; i < numberofpayments; i++)
                    {
                        paymentDetail = new PaymentDetail();
                        paymentDetail.PaymentDate = startDate;
                        paymentDetail.PaymentAmount = model.PaymentAmount;
                        paymentDetail.RecurringPaymentFk = result;
                        paymentDetail.Description = model.Description;
                        paymentDetailList.Add(paymentDetail);
                        startDate = startDate.AddMonths(1);
                    }

                }
                else if (frequency.Name == "Yearly")
                {
                    DateTime startDate = model.StartDate;
                    for (int i = 0; i < numberofpayments; i++)
                    {
                        paymentDetail = new PaymentDetail();
                        paymentDetail.PaymentDate = startDate;
                        paymentDetail.PaymentAmount = model.PaymentAmount;
                        paymentDetail.RecurringPaymentFk = result;
                        paymentDetail.Description = model.Description;
                        paymentDetailList.Add(paymentDetail);
                        startDate = startDate.AddYears(1);
                    }

                }
                var detailResult = _recurringPaymentManager.SaveUpdatePaymentDetailList(paymentDetailList);



                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}