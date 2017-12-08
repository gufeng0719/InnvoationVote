using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InnvoationVote.Startup))]
namespace InnvoationVote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
