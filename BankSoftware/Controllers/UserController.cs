using BankSoftware.Utilities;
using BankSoftwareManager.IManager;
using BankSoftwareManager.Manager;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TwoFactorAuthNet;

namespace BankSoftware.Controllers
{
    public class UserController : BaseController
    {
       
        private readonly IUserManager _userManager;
        private readonly IBankManager _bankManager;
        public UserController(ISessionManager sessionManager, IUserManager userManager,IBankManager bankManager) : base(sessionManager)
        {
            this._userManager = userManager;
            this._bankManager = bankManager;
        }
        // GET: Account Login
        public ActionResult Login()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var isSessionExist = sessionManager.CurrentUser != null ? true : false;
            
            if (isSessionExist)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult GetAllUser()
        {
            try 
            {
                var result = _userManager.GetAllUser();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public ActionResult GetUserById(Guid userId)
        {
            try
            {
                var result = _userManager.GetUserById(userId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult SaveUpdateUser(User model)
        {
            try
            {
                Common util = new Common();
                if (model.Password != null)
                {
                    var encoded = Common.EncodePasswordToBase64(model.Password);
                    if (encoded != null)
                    {
                        model.Password = encoded;
                    }
                }
                var result = _userManager.SaveUpdateUser(model);
                return new JsonResult() { Data = result };
            }
            catch (Exception e)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            try
            {
                string qrCode = "";
                Common util = new Common();
                if (model.Password != null)
                {
                    var encoded = Common.EncodePasswordToBase64(model.Password);
                    if (encoded != null)
                    {
                        model.Password = encoded;
                    }
                }
                var user = _userManager.LoginUser(model.Email,model.Password);
                var bankDetail = _bankManager.GetAllBanks().FirstOrDefault();
                if (user.Model != null)
                {
                    TwoFactorAuth tfa = new TwoFactorAuth("BankManagement");
                    string secret = "";
                    if (user.Model.Secret == null)
                    {
                        secret = tfa.CreateSecret(160);
                    }
                    else
                    {
                        secret = user.Model.Secret;
                    }
                    //// Though the default is an 80 bits secret (for backwards compatibility reasons) we 
                    //// recommend creating 160+ bits secrets (see RFC 4226 - Algorithm Requirements)
                    //OTP: To display QRCODE Image on popup
                    user.ErrorMessage = tfa.GetQrCodeImageAsDataUri(model.Email, secret);
                    ViewBag.QRCODE = qrCode;
                    sessionManager.LoggedInUser = user.Model;
                    sessionManager.UserSecret = secret;
                    //Session["CurrentUser"] = user.Model;
                    Session["BankDetail"] = bankDetail;
                }
                return new JsonResult() { Data = user  };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = null };
            }

        }
        //VerifyOneTimePassword

        [HttpPost]
        public ActionResult VerifyOneTimePassword(User model)
        {
            try
            {
                TwoFactorAuth tfa = new TwoFactorAuth("BankManagement");
                var authentic = tfa.VerifyCode(sessionManager.UserSecret, model.Password);
                if (authentic == true)
                {
                    sessionManager.CurrentUser = sessionManager.LoggedInUser;
                    model = sessionManager.CurrentUser;
                    model.Secret = sessionManager.UserSecret;
                    //OTP: To save for Secret Code in database
                    var result = _userManager.SaveUpdateUser(model);
                    return new JsonResult() { Data = "Authentic" };
                }
                else {
                    sessionManager.CurrentUser = null;
                    sessionManager.LoggedInUser = null;
                    sessionManager.UserSecret = null;
                }
                
                return new JsonResult() { Data = "NotAuthentic" };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = "NotAuthentic" };
            }

        }

        public ActionResult Logout()
        {
            sessionManager.CurrentUser = null;
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult ForgetPassword(User model)
        {
            var resetCode = Guid.NewGuid();

            var verifyUrl = "/User/ResetPassword/" + resetCode;

            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var getUser = _userManager.GetUserByEmailId(model.Email);
            if (getUser != null)
            {
                getUser.ResetPasswordId = resetCode;

                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                var result = _userManager.SaveUpdateUser(getUser);


                var subject = "Password Reset Request";
                var body = "Hi " + getUser.Name + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " +

                     " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                     "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

                SendEmail(getUser.Email, body, subject);

                ViewBag.Message = "Reset password link has been sent to your email id.";
            }
            else
            {
                ViewBag.Message = "User doesn't exists.";
                return View();
            }
            //get user details from database.
           

            return View();
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("sdeveloper401@gmail.com");
                mail.To.Add(emailAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("sdeveloper401@gmail.com", "SoftwareE@2024");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }


    }
}