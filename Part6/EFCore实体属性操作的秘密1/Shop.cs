using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class Shop
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Geo Location { get; set; }
    }
}
