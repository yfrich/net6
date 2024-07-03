using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class OrderDetail
    {
        public Order Order { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        //public Merchan Merchan { get; set; }
        public long MerchanId { get; set; }//跨聚合进行实体引用，只能引用根实体，并且只能引用ID 而不是对象
        public int Count { get; set; }
    }
}
