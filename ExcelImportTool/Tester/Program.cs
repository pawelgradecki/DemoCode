using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo newFile = new FileInfo("test.xlsx");

            ExcelPackage pck = new ExcelPackage(newFile);
        }
    }
}
