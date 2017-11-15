using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Odx.Crm.Core.Utilities
{
    static class InterfaceMocker       
    {
        public static T Mock<T>(Entity entity)
        {
            var type = ProxyTypesCache.GetProxyType(entity.LogicalName);
            var copy = Activator.CreateInstance(type);
            (copy as Entity).Attributes = entity.Attributes;
            (copy as Entity).Id = entity.Id;
            if (typeof(T).IsAssignableFrom(type))
            {
                return (T)copy;
            }
            else
            {
                throw new InvalidPluginExecutionException("Cannot mock interface. Target type does not implement interface " + typeof(T).Name);
            }            
        }
    }
}
