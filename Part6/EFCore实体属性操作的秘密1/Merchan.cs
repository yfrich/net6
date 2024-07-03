using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    //商品
    internal class Merchan: IAggreagateRoot
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
