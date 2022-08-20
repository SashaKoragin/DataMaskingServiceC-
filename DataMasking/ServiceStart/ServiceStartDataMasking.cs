using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Threading;
using DataMasking.Service;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using SignalRLibary.SignalR;

[assembly: OwinStartup(typeof(ConfigStartSignalR))]
namespace DataMasking.ServiceStart
{
    partial class ServiceStartDataMasking : ServiceBase
    {
        public ServiceStartDataMasking()
        {
            InitializeComponent();
        }
        public IDisposable ServerSignalRDataMasking { get; set; }
        public ServiceHost ServiceHostDataMasking { get; set; }


        protected override void OnStart(string[] args)
        {
            var url = "http://+:8059";
            ServerSignalRDataMasking = WebApp.Start(url);
            ServiceHostDataMasking = new WebServiceHost(typeof(ServiceDataMasking));
            ServiceHostDataMasking.Open();
            
            new Thread(StartService).Start();
        }

        void StartService()
        {
            Loggers.Log4NetLogger.Info(new Exception("Запустили сервис маскирования данных!"));
        }


        protected override void OnStop()
        {
            if (ServiceHostDataMasking != null)
            {
                ServiceHostDataMasking.Close();
                ServiceHostDataMasking = null;
            }
            ServerSignalRDataMasking?.Dispose();
        }
    }
}
