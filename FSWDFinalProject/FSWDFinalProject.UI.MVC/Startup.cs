using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSWDFinalProject.UI.MVC.Startup))]
namespace FSWDFinalProject.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
