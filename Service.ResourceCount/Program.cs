using System.ServiceProcess;

namespace BT.Service.ResourceCount
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
            { 
                new HomoryResourceCountService() 
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
