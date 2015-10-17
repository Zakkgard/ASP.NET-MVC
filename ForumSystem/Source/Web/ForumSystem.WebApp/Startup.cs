using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumSystem.WebApp.Startup))]
namespace ForumSystem.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
