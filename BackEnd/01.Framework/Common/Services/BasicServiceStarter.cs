using Common;
using Common.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;

namespace PA.Framework
{
    /// <summary>
    /// http://www.devopsonwindows.com/build-windows-service-framework/
    /// </summary>
    public static class BasicServiceStarter
    {
        public static void Run<T>(string serviceName = "StandardService") where T : IService, new()
        {
            try
            {

                if (Environment.UserInteractive)
                {
                    var cmd = Environment.GetCommandLineArgs()
                        .Skip(1)
                        .FirstOrDefault()
                        ?.ToLower();
                    switch (cmd)
                    {
                        case "pai":
                            BasicServiceInstaller.Install(serviceName);
                            ServiceController service = new ServiceController(serviceName);
                            service.Start();
                            service.WaitForStatus(ServiceControllerStatus.Running);
                            using (var s = new T())
                            {
                                s.Start(Environment.GetCommandLineArgs());
                            }
                            Console.WriteLine("Done");
                            break;
                        case "pa-u":
                            BasicServiceInstaller.Uninstall(serviceName);
                            break;
                        default:
                            using (var s = new T())
                            {
                                s.Start(Environment.GetCommandLineArgs());
                            }
                            break;
                    }
                }
                else
                {
                    ServiceBase.Run(new BasicService<T> { ServiceName = serviceName });
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex);
            }
            
        }
    }
}
