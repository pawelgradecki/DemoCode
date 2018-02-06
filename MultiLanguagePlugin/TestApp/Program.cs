using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stream = Assembly.GetAssembly(typeof(MultiLanguagePlugin.MultiLanguagePlugin)).GetManifestResourceStream($"MultiLanguagePlugin.Resources.pl-PL.resources");
            using (stream)
            {
                var reader = new ResourceReader(stream);
                var en = reader.GetEnumerator();
                while (en.MoveNext())
                {
                    if (en.Key.Equals("helloWorld"))
                    {
                        var message = en.Value.ToString();
                    }
                }
            }
        }
    }
}
