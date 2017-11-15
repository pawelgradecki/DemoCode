using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.Model;

namespace Multientityplugin
{
    public class CalculateTotalAmountAccountPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            var serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            var service = serviceFactory.CreateOrganizationService(context.UserId);

            var account = (context.InputParameters["Target"] as Entity).ToEntity<Account>();
            account.new_totalsum = account.new_netamount + account.new_margin;
        }
    }
}
