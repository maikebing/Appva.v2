using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Appva.Mcss.Admin.Startup))]
namespace Appva.Mcss.Admin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthenticationConfiguration.Register(app);
        }
    }
}
