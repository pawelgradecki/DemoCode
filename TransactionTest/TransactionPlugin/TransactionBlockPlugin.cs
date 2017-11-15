using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TransactionPlugin
{
    public class TransactionBlockPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var service = factory.CreateOrganizationService(null);

            var target = pluginContext.InputParameters["Target"] as Entity;

            if (target.GetAttributeValue<bool?>("new_blockexecution") ?? false)
            {
                var blocker = new Entity("new_lock");
                blocker.Id = new Guid("54A125B8-FFA8-E711-A821-000D3A2A7FF5");
                blocker["new_entityname"] = "contact";
                tracingService.Trace($"Uwaga, odsunąć się, zaraz będę mielić! {DateTime.Now.ToString("hh:MM:ss.fff")}");
                service.Update(blocker);
            }
            
            tracingService.Trace($"Zaczynam mielić! {DateTime.Now.ToString("hh:MM:ss.fff")}");
            Thread.Sleep(10000);
            tracingService.Trace($"Zmieliłem! {DateTime.Now.ToString("hh:MM:ss.fff")}");
        }
    }
}
