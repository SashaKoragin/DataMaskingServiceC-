using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace SignalRLibary.SignalR
{
   public class ConfigStartSignalR
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromHours(8);
                GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromHours(5);
                GlobalHost.HubPipeline.AddModule(new HubError.HubError());
                app.Map("/signalr", map =>
                {
                    
                    map.UseCors(CorsOptions.AllowAll);
                    var hubConfiguration = new HubConfiguration
                    {

                        EnableDetailedErrors = true,
                        
                        
                    };
                    map.RunSignalR(hubConfiguration);
                });
                
                Loggers.Log4NetLogger.Info(new Exception("Конфигурацию SignalR запустили!!!"));
            }
            catch (Exception e)
            {
                Loggers.Log4NetLogger.Error(e);
            }
        }
    }
}
