using Microsoft.Owin;
using Owin;
using WebService.Worker;

[assembly: OwinStartupAttribute(typeof(WebService.Startup))]
namespace WebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            ReservationWorker.GetInstance();
        }
    }
}
