using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace DataMasking.ServiceStart
{
    [RunInstaller(true)]
    public partial class ServiceInstallerDataMasking : Installer
    {
        public ServiceInstallerDataMasking()
        {
            var process = new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem };
            var service = new ServiceInstaller
            {
                
                ServiceName = "ServiceDataMasking",
                Description = "Служба Api по маскированию данных!!!"
            };
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
