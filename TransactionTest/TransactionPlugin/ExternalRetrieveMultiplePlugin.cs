using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionPlugin
{
    public class ExternalRetrieveMultiplePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace("Execute Retrieve Multiple");
            var retrieveMultipleResponse = new RetrieveMultipleResponse();
            retrieveMultipleResponse.Results = pluginContext.OutputParameters;
            
            for (int i = 0; i < 10; i++)
            {
                var result = new Entity("new_virtualentityprovider");
                result["new_virtualentityproviderid"] = Guid.NewGuid();
                result["new_name"] = $"result{i}";
                result["new_value"] = $"value{i}";
                retrieveMultipleResponse.EntityCollection.Entities.Add(result);
            }           
        }
    }
}
