using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.Utilities;

namespace Multientityplugin
{
    public static class Extensions
    {
        public static T Mock<T>(this Entity entity)
        {
            return InterfaceMocker.Mock<T>(entity);
        }
    }
}
