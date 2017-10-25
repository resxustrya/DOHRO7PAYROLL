using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOH7PAYROLL.Startup))]
namespace DOH7PAYROLL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
