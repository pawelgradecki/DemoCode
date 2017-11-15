using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;

namespace DemoCodeNoProxy
{
    public class FinalEntity : Entity
    {
        public FinalEntity(string entityName) : base(entityName) { }

        protected string GetAttributeName(string propertyName)
        {
            return "odx_" + propertyName.ToLowerInvariant();
        }

        protected T GetAttribute<T>([CallerMemberName]string propertyName = null)
        {
            return this.GetAttributeValue<T>(GetAttributeName(propertyName));
        }

        protected void SetAttribute(object value, [CallerMemberName]string propertyName = null)
        {
            this.SetAttributeValue(GetAttributeName(propertyName), value);
        }
    }
}