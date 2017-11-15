using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoCodeNoProxy
{
    public static class Extensions
    {
        public static TAttribute GetCustomAttribute<TAttribute>(this Type type)
        where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            
            return att;
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this PropertyInfo propertyInfo)
        where TAttribute : Attribute
        {
            var att = propertyInfo.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;

            return att;
        }
    }
}
