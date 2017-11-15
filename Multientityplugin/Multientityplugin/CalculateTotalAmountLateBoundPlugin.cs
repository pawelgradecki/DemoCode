using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Multientityplugin
{
    public class CalculateTotalAmountLateBoundPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            var serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            var service = serviceFactory.CreateOrganizationService(context.UserId);

            var lateBoundEntity = context.InputParameters["Target"] as Entity;
            lateBoundEntity["new_totalsum"] = lateBoundEntity.GetAttributeValue<decimal>("new_netamount") + lateBoundEntity.GetAttributeValue<decimal>("new_margin");
        }
    }
}
