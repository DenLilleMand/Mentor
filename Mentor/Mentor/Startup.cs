using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mentor.Startup))]
namespace Mentor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
