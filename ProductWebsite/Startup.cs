using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductWebsite.Startup))]
namespace ProductWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
