using System.Linq;
using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;
using Microsoft.Xrm.Sdk.Client;
using System.Collections.Generic;
using System;

namespace DemoCodeNoProxy
{
    public class MyEntity : Entity
    {
        private Dictionary<string, string> logicalNamesDictionary;

        public MyEntity()
        {
            this.LogicalName = this.GetType().GetCustomAttribute<EntityLogicalNameAttribute>().LogicalName;
            this.logicalNamesDictionary = this.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetCustomAttribute<AttributeLogicalNameAttribute>()?.LogicalName);
        }

        protected string GetAttributeName(string propertyName)
        {
            try
            {
                return this.logicalNamesDictionary[propertyName] ?? propertyName.ToLowerInvariant();
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(propertyName);
            }
        }

        protected T GetAttribute<T>([CallerMemberName]string propertyName = null)
        {
            return this.GetAttributeValue<T>(GetAttributeName(propertyName));
        }

        protected void SetAttribute(object value, [CallerMemberName]string propertyName = null)
        {
            this.SetAttributeValue(GetAttributeName(propertyName), value);
        }

        protected T GetOptionSetValue<T>([CallerMemberName]string propertyName = null)
        {
            var optionSetValue = this.GetAttribute<OptionSetValue>(propertyName);
            return (T)(object)optionSetValue.Value;
        }

        protected void SetOptionSetValue(object value, [CallerMemberName]string propertyName = null)
        {
            this.SetAttribute(new OptionSetValue((int)value), propertyName);
        }
    }
}