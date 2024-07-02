using System;
using System.Linq;

namespace EFCore一对一
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                Order order = new Order();
                order.Name = "书";
                Delivery delivery = new Delivery();
                delivery.Number = "123123";
                delivery.CompanyName = "鱼鱼快递";
                delivery.Order = order;
                db.Deliverys.Add(delivery);
                db.SaveChanges();
                */

                var orders = db.Orders.Where(t => t.Delivery.CompanyName == "鱼鱼快递");
                foreach (var item in orders)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
    }
}
