using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCRiceStore.Startup))]
namespace MVCRiceStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
