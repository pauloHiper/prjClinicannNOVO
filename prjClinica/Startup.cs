using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prjClinica.Startup))]
namespace prjClinica
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
