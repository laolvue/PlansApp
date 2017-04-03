using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlansApp.Startup))]
namespace PlansApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
