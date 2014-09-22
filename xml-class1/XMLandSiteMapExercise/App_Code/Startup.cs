using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XMLandSiteMapExercise.Startup))]
namespace XMLandSiteMapExercise
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
