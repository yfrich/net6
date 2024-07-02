using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对一
{
    class Order
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Delivery Delivery { get; set; }
    }
}
