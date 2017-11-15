using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DemoCodeNoProxy
{
    public class MyAccountRepository
    {
        private IOrganizationService service;

        public MyAccountRepository(IOrganizationService service)
        {
            this.service = service;
        }

        public List<MyAccount> GetAccounts()
        {
            using (var ctx = new OrganizationServiceContext(this.service))
            {
                return ctx.CreateQuery<MyAccount>()
                    .Where(x => x.ClientCode != null)
                    .ToList();
            }
        }
    }
}
