using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multientityplugin.Interfaces
{
    interface IEntityWithTotalSum
    {
        decimal new_totalsum { get; set; }
        decimal new_netamount { get; set; }
        decimal new_margin { get; set; }
    }
}
