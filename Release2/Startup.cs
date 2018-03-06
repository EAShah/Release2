using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Release2.Startup))]
namespace Release2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
