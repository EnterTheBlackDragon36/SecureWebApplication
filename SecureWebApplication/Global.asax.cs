using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SecureWebApplication;

namespace SecureWebApplication
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Application_Authenticate(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = (id.Ticket);
                        if (!FormsAuthentication.CookiesSupported)
                        {
                            //If cookie is not supported for forms authentication, then the
                            //authentication ticket is stored in the URL, which is encrypted.
                            //So, decrypt it
                            ticket = FormsAuthentication.Decrypt(id.Ticket.Name);
                        }
                        // Get the stored user-data, in this case, user roles
                        if (!string.IsNullOrEmpty(ticket.UserData))
                        {
                            string userData = ticket.UserData;
                            string[] roles = userData.Split(',');
                            //Roles were put in the UserData property in the authentication ticket
                            //while creating it
                            HttpContext.Current.User =
                              new System.Security.Principal.GenericPrincipal(id, roles);
                        }
                    }
                }
            }



        }

    }
}
