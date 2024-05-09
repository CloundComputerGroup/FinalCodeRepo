using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwoFactorAuthNet;

namespace BankSoftware.Utilities
{
    public class EmailService 
    {
        public EmailService()
        {
            TwoFactorAuth tfa = new TwoFactorAuth("MyCompany");
            // Though the default is an 80 bits secret (for backwards compatibility reasons) we 
            // recommend creating 160+ bits secrets (see RFC 4226 - Algorithm Requirements)
            string secret = tfa.CreateSecret(160);
            var qrCode = tfa.GetQrCodeImageAsDataUri("Bob Ross", secret);
            // Verify code
            //var verifyingResult = tfa.VerifyCode((string)Session["secret"], code);

        }
       
        //public Task SendAsync(IdentityMessage message)
        //{
        //    string text = message.Body;
        //    string html = message.Body;
        //    //do whatever you want to the message        
        //    MailMessage msg = new MailMessage();
        //    msg.From = new MailAddress("scott@hanselman.com");
        //    msg.To.Add(new MailAddress(message.Destination));
        //    msg.Subject = message.Subject;
        //    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
        //    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

        //    SmtpClient smtpClient = new SmtpClient("smtp.whatever.net", Convert.ToInt32(587));
        //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(Keys.EmailUser, Keys.EMailKey);
        //    smtpClient.Credentials = credentials;
        //    smtpClient.Send(msg);

        //    return Task.FromResult(0);
        //}
    }
}