using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionPlugin
{
    public class ExternalRetrievePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var retrieveResponse = new RetrieveResponse();
            retrieveResponse.Results = pluginContext.OutputParameters;
            retrieveResponse.Entity["new_name"] = "TEST";
            retrieveResponse.Entity["new_value"] = "VALUE";
        }
    }
}
