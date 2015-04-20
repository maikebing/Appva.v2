using System.Web.Mvc;

namespace Appva.Mcss.Admin.Areas.sdfsdfsdf
{
    public class sdfsdfsdfAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "sdfsdfsdf";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "sdfsdfsdf_default",
                "sdfsdfsdf/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}