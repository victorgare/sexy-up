using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SexyUp.Web.Startup))]
namespace SexyUp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
