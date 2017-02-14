using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Site.OnlineStore.Startup))]
namespace Site.OnlineStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
