using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionPlugin
{
    public class BasicTransactionPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var pluginContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = factory.CreateOrganizationService(null);

            var target = pluginContext.InputParameters["Target"] as Entity;
            var isBad = false;
            var allRelatedOpportunities = GetAllRelatedOpportunities(service, target);
            allRelatedOpportunities.ForEach(opportunity =>
            {
                var task = new Entity("task");
                task["subject"] = "Sprawdź oportunjcę, coś się święci!";
                task["regardingobjectid"] = opportunity.ToEntityReference();
                service.Create(task);
            });

            //jeszcze coś ważnego

            //i coś ważniejszego

            //ooops coś jednak jest źle, nie chcemy tego bajzlu
            if (isBad)
            {
                throw new InvalidPluginExecutionException("Robiłem, robiłem i nic nie zrobiłem :(");
            }
        }

        private List<Entity> GetAllRelatedOpportunities(IOrganizationService service, Entity target)
        {
            throw new NotImplementedException();
        }
    }
}
