using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MultiLanguagePlugin
{
    public class MultiLanguagePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service = serviceFactory.CreateOrganizationService(Guid.Empty);

            var querySettings = new QueryExpression("usersettings");
            querySettings.ColumnSet.AddColumns("localeid");
            querySettings.Criteria.AddCondition("systemuserid", ConditionOperator.Equal, context.InitiatingUserId);
            var settings = service.RetrieveMultiple(querySettings).Entities.First();

            var message = "test";
            // culture-specific file, i.e. "LangResources.fr"
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MultiLanguagePlugin.Resources.pl-PL.resources");
            if (null == stream)
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MultiLanguagePlugin.Resources.resources");
            }

            ResourceReader reader = new ResourceReader(stream);
            var en = reader.GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Key.Equals("helloWorld"))
                {
                    message = en.Value.ToString();
                }
            }

            throw new InvalidPluginExecutionException(message);
        }

        internal static string GetString(string str, string lang)
        {

            if (string.IsNullOrEmpty(str)) throw new ArgumentNullException("empty language query string");
            if (string.IsNullOrEmpty(lang)) throw new ArgumentNullException("no language resource given");

            // culture-specific file, i.e. "LangResources.fr"
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MultiLanguagePlugin.Resources.{lang}.resources");

            // resource not found, revert to default resource
            if (null == stream)
            {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MultiLanguagePlugin.Resources.resources");
            }

            ResourceReader reader = new ResourceReader(stream);
            var en = reader.GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Key.Equals(str))
                {
                    return en.Value.ToString();
                }
            }

            // string not translated, revert to default resource
            return Resources.ResourceManager.GetString(str);
        }
    }
}
