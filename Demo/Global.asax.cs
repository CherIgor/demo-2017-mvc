using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // For security reasons
            MvcHandler.DisableMvcResponseHeader = true;
        }

        // For security reasons
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            Context.Response.Headers.Remove("X-Powered-By");
            Context.Response.Headers.Remove("X-AspNet-Version");
            Context.Response.Headers.Remove("X-AspNetMvc-Version");
            Context.Response.Headers.Remove("Server");
        }
    }
}