using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.Model;

namespace Multientityplugin
{
    public class CalculateTotalAmountEarlyBoundBADPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            var serviceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            var service = serviceFactory.CreateOrganizationService(context.UserId);

            var lateBoundEntity = context.InputParameters["Target"] as Entity;
            switch (lateBoundEntity.LogicalName)
            {
                case Account.EntityLogicalName:
                    var account = lateBoundEntity.ToEntity<Account>();
                    account.new_totalsum = account.new_netamount + account.new_margin;
                    break;
                case Opportunity.EntityLogicalName:
                    var opportunity = lateBoundEntity.ToEntity<Opportunity>();
                    opportunity.new_totalsum = opportunity.new_netamount + opportunity.new_margin;
                    break;
                default:
                    break;
            }
        }
    }
}
