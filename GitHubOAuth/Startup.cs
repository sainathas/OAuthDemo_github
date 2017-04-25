using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GitHubOAuth.Startup))]
namespace GitHubOAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
