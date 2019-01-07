using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Futbol.Startup))]
namespace Futbol
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
