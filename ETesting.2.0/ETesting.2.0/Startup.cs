using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ETesting._2._0.Startup))]
namespace ETesting._2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
