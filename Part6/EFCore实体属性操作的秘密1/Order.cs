using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFCore实体属性操作的秘密1
{
    internal class Order: IAggreagateRoot
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public double TotalAmount { get; set; }
        public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        public void AddDetail(Merchan merchan, int count)
        { 
            /*
            var detail = Details.SingleOrDefault(t => t.Id == merchan.Id);
            if (detail == null)
            {
                detail = new OrderDetail { Merchan = merchan, Count = count };
                Details.Add(detail);
            }
            else
            {
                detail.Count += count;
            }
            */
        }
    }
}
