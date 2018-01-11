using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserRolesTest.Startup))]
namespace UserRolesTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
