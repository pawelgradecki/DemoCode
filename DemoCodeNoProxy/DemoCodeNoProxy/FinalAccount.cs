using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;

namespace DemoCodeNoProxy
{
    [Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("account")]
    public class FinalAccount : FinalEntity
    {
        public FinalAccount() : base("account") { }

        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("odx_clientcode")]
        public string ClientCode
        {
            get => GetAttribute<string>();
            set => SetAttribute(value);
        }
    }
}