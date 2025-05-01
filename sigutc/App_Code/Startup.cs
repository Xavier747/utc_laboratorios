using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sigutc.Startup))]
namespace sigutc
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
