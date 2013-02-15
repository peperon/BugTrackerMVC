using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BugTracker.Infrastructure
{
    public class ApplicationSecurity
    {
        public static void AddAuthenticationCookie(string userName, string role, bool isPersistent)
        {
            // keep cookies in sync 
            System.DateTime currentDateTime = DateTime.Now;
            System.DateTime expirationDateTime;
            if (isPersistent)
                expirationDateTime = currentDateTime.AddYears(3);
            else
                expirationDateTime = currentDateTime.AddMinutes(240);

            // create and encrypt a cookie authentication ticket ( version, user name, issue time, expires every hour, don't persist cookie )             
            FormsAuthenticationTicket formTicket = new FormsAuthenticationTicket(1, userName, currentDateTime, expirationDateTime, isPersistent, role, FormsAuthentication.FormsCookiePath);
            AddAuthenticationCookie(formTicket);
        }

        public static void AddAuthenticationCookie(FormsAuthenticationTicket formTicket)
        {
            string encryptedTicket = FormsAuthentication.Encrypt(formTicket);
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (formTicket.IsPersistent)
                authenticationCookie.Expires = formTicket.IssueDate.AddYears(3);

            HttpContext.Current.Response.Cookies.Add(authenticationCookie);
        }

        public static bool IsPersistent(string userName)
        {
            HttpCookie authenticateCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            string encryptedTicket = authenticateCookie.Value;
            FormsAuthenticationTicket formTicket = FormsAuthentication.Decrypt(encryptedTicket);
            return formTicket.IsPersistent;
        }

        public static void RenewAuthenticationCookie()
        {
            HttpCookie authenticateCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            string encryptedTicket = authenticateCookie.Value;

            FormsAuthenticationTicket formTicket = FormsAuthentication.Decrypt(encryptedTicket);

            if (formTicket.IsPersistent) // don't renew ticket otherwise it won't be persistent any more
                return;

            FormsAuthenticationTicket renewedFormTicket = FormsAuthentication.RenewTicketIfOld(formTicket);

            encryptedTicket = FormsAuthentication.Encrypt(renewedFormTicket);
            HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Value = encryptedTicket;
        }

        public static void SignOut()
        {
            HttpContext.Current.Session.Abandon();

            // Log User Off from Cookie Authentication System 
            FormsAuthentication.SignOut();

            // expire cookies 
            HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
            HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Path = FormsAuthentication.FormsCookiePath;
            HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-30);
        }
    }
}