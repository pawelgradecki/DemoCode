using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Multientityplugin.Interfaces;
using Odx.Crm.Core.Model;

namespace Multientityplugin
{
    public class CalculateTotalAmountEarlyBoundPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            var serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            var service = serviceFactory.CreateOrganizationService(context.UserId);

            var earlyBound = (context.InputParameters["Target"] as Entity).Mock<IEntityWithTotalSum>();
            earlyBound.new_totalsum = earlyBound.new_netamount + earlyBound.new_margin;          
        }
    }
}
