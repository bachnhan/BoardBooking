using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoardBooking.Startup))]
namespace BoardBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
