using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace TransactionPlugin
{
    public class Autonumber : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(null);

            var target = pluginContext.InputParameters["Target"] as Entity;

            var qExpression = new QueryExpression("new_lock");
            qExpression.ColumnSet = new ColumnSet("new_name", "new_lastnumber");
            qExpression.Criteria.AddCondition("new_name", ConditionOperator.Equal, "Counter");
            var results = service.RetrieveMultiple(qExpression);
            var counter = results.Entities.First();
            var blocker = new Entity("new_lock");
            blocker.Id = counter.Id;
            blocker["new_name"] = "Counter";
            service.Update(blocker);

            //var qExpression2 = new QueryExpression("new_lock");
            //qExpression2.ColumnSet = new ColumnSet("new_lastnumber");
            //qExpression2.Criteria.AddCondition("new_name", ConditionOperator.Equal, "Counter");
            //var lockedResults = service.RetrieveMultiple(qExpression2);
            //var lockedCounter = results.Entities.First();

            //var rmRequest = new RetrieveMultipleRequest();
            //rmRequest.Query = qExpression2;
            //var lockedResults = (RetrieveMultipleResponse)service.Execute(rmRequest);
            //var lockedCounter = lockedResults.EntityCollection.Entities.First();

            var lockedCounter = service.Retrieve("new_lock", blocker.Id, new ColumnSet("new_lastnumber"));

            var currentNumber = lockedCounter.GetAttributeValue<int>("new_lastnumber");

            target["accountnumber"] = $"{++currentNumber}";

            var counterUpdater = new Entity("new_lock");
            counterUpdater.Id = counter.Id;
            counterUpdater["new_lastnumber"] = currentNumber;
            service.Update(counterUpdater);
        }
    }
}
