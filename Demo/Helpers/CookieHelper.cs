using System;
using System.Web;
using System.Web.Security;

namespace Demo.Helpers
{
    internal static class CookieHelper
    {
        private const string UserNameCookieName = ".Demo.UserName";

        public static string GetNameFromCookies(HttpContextBase httpContext)
        {
            string name = null;
            var encTicket = httpContext.Request.Cookies[UserNameCookieName];
            if (encTicket != null)
            {
                try
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket.Value);
                    if (ticket?.Expiration > DateTime.Now)
                    {
                        name = ticket.UserData;
                    }
                }
                catch
                {
                    // TODO: Log error.
                }
            }

            return name;
        }

        public static void SetNameToCookies(HttpResponseBase response, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User name must be specified", nameof(userName));
            }

            const int ticketVersion = 1;
            const bool isPersistent = true;
            // In minutes
            const int expirationTime = 30; // TODO: Move to Web.config

            var userData = userName;
            var ticket = new FormsAuthenticationTicket(
                ticketVersion,
                userName,
                DateTime.Now,
                DateTime.Now.AddMinutes(expirationTime),
                isPersistent,
                userData,
                FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            response.Cookies.Add(new HttpCookie(UserNameCookieName, encTicket));
        }
    }
}