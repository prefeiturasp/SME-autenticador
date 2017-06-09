using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutenticadorWebSite.Startup))]

namespace AutenticadorWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}