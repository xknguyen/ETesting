using System.Web.Mvc;

namespace ETesting._2._0.Areas.Adm
{
    public class AdmAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Adm";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Adm_default",
                "Adm/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ETesting._2._0.Areas.Adm.Controllers" }
            );
        }
    }
}