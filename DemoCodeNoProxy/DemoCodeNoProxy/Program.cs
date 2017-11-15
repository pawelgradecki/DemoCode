using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCodeNoProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CrmServiceClient(ConfigurationManager.ConnectionStrings["crm"].ConnectionString);

            var accountRepository = new MyAccountRepository(service);

            var result = accountRepository.GetAccounts();
            Console.ReadLine();
        }
    }
}
