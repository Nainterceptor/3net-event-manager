using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_3NET_SyncWorld.Startup))]
namespace _3NET_SyncWorld
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
