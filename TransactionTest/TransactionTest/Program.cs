using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace TransactionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRM"].ConnectionString);
            var executeTransactionRequest = new ExecuteTransactionRequest();
            executeTransactionRequest.Requests.Add(new CreateAttributeRequest()/* nowe pole w encji */);
            executeTransactionRequest.Requests.Add(new CreateAttributeRequest()/* nowe pole w encji */);
            executeTransactionRequest.Requests.Add(new CreateAttributeRequest()/* nowe pole w encji */);
            executeTransactionRequest.Requests.Add(new CreateAttributeRequest()/* nowe pole w encji */);
            service.Execute(executeTransactionRequest);


            var createAutonumberAttribute = new CreateAttributeRequest
            {
                EntityName = "new_customentity",
                Attribute = new StringAttributeMetadata
                {
                    AutoNumberFormat = "{MMDDYY}-{SEQNUM:8}-{RANDSTRING:4}",
                    LogicalName = "new_entitynumber",
                    SchemaName = "new_entitynumber",
                    MaxLength = 100,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    FormatName = StringFormatName.Text,
                    DisplayName = new Label("Autonumber", 1033),
                    Description = new Label("Autonumber", 1033)
                }
            };

            service.Execute(createAutonumberAttribute);
            

            Thread thread1 = new Thread(new ThreadStart(DoImport));
            Thread thread2 = new Thread(new ThreadStart(DoImport));


            thread1.Start();
            thread2.Start();
        }

        public static void DoImport()
        {
            var service = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRM"].ConnectionString);

            for (int i = 0; i < 50; i++)
            {
                var accToCreate = new Entity("account");
                accToCreate["name"] = "test";
                service.Create(accToCreate);
                Console.WriteLine(i);
            }
        }
    }
}
