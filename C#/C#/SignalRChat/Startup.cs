using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<OnlySignalRMiddleware>();
            app.MapSignalR();
        }
        
        public class OnlySignalRMiddleware : OwinMiddleware
        {
            public OnlySignalRMiddleware(OwinMiddleware next) : base(next) { }

            public override Task Invoke(IOwinContext context)
            {
                if (!context.Request.Path.HasValue || context.Request.Path.Value.StartsWith("/signalr"))
                    return Next.Invoke(context); //continue the pipeline

                context.Response.StatusCode = 200;
                return context.Response.WriteAsync(""); //terminate the pipeline
            }
        }
    }
}