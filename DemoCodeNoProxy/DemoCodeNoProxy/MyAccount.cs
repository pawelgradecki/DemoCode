using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.CompilerServices;

namespace DemoCodeNoProxy
{
    [EntityLogicalName("account")]
    public class MyAccount : MyEntity
    {
        public MyAccount()
        {
            this.LogicalName = typeof(MyAccount).GetCustomAttribute<EntityLogicalNameAttribute>().LogicalName;
        }

        [AttributeLogicalName("odx_clientcode")]
        public string ClientCode
        {
            get => GetAttribute<string>();
            set => SetAttribute(value);
        }
    }
}