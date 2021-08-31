using System.Web.Mvc;

namespace RING.Areas.RING
{
    public class RINGAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RING";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RING_default",
                "RING/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}