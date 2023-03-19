using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MangaWorld_admin.Startup))]
namespace MangaWorld_admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
