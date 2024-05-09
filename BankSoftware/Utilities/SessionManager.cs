using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace BankSoftware.Utilities
{
    public class SessionManager : ISessionManager
    {
        public User CurrentUser
        {
            get { return HttpContext.Current.Session.GetDataFromSession<User>("CurrentUser"); }
            set { HttpContext.Current.Session.SetDataToSession("CurrentUser", value); }
        }


        public User LoggedInUser
        {
            get { return HttpContext.Current.Session.GetDataFromSession<User>("LoggedInUser"); }
            set { HttpContext.Current.Session.SetDataToSession("LoggedInUser", value); }
        }

        public string UserSecret
        {
            get { return HttpContext.Current.Session.GetDataFromSession<string>("UserSecret"); }
            set { HttpContext.Current.Session.SetDataToSession("UserSecret", value); }
        }

        //public string GetUserWelcomeNote()
        //{
        //   // string welcomenote = string.Format("Logged in as: {0}", LoggedInUser.Name);
        //    if (CurrentUser != null && CurrentUser.UserPk != LoggedInUser.UserPk)
        //    {
        //        welcomenote = welcomenote + string.Format(" (Viewing as: {0})", CurrentUser.Name);
        //    }
        //    return welcomenote;
        //}
    }

    internal static class SessionExtensions
    {
        public static T GetDataFromSession<T>(this HttpSessionState session, string key)
        {
            return (T)session[key];
        }

        public static void SetDataToSession(this HttpSessionState session, string key, object value)
        {
            session[key] = value;
        }
    }
}