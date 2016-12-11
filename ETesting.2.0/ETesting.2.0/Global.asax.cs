using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TwitterBootstrapMVC;
using WebCore.Binder;

namespace ETesting._2._0
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrap.Configure();
            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableCustomDateBinder());
        }
    }
}
