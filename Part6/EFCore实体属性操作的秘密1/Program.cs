using EFCore实体属性操作的秘密1;
using Zack.Infrastructure.EFCore;


//实体属性操作的秘密
/*
//Person p1 = new Person();

//p1.Name = "rich";
//using var ctx = new MyDbContext();
//ctx.Persons.Add(p1);
//ctx.SaveChanges();
*/

/*
using var ctx = new MyDbContext();
Person p = ctx.Persons.First();
Console.WriteLine(p.Id);
Console.WriteLine(p.Name);
*/

/*
Person p1 = new Person();

p1.ChangeName("rich");
using var ctx = new MyDbContext();
ctx.Persons.Add(p1);
ctx.SaveChanges();
*/

using var ctx = new MyDbContext();
//EFCore的充血模型
/*
User user = new User("rich");
user.ChangePasswordHash("123456");
ctx.Users.Add(user);
ctx.SaveChanges();
*/
/*
var user = ctx.Users.First();
Console.WriteLine(user.Remark);
*/

//EFCore 枚举类型
/*
ValueObject1 object1 = new ValueObject1 { Name = "hhh", Currency = CurrencyName.CNY };
ValueObject1 object2 = new ValueObject1 { Name = "hhh", Currency = CurrencyName.USD };
ValueObject1 object3 = new ValueObject1 { Name = "hhh", Currency = CurrencyName.NZD };
//ctx.ValueObject1s.Add(object1);
//ctx.ValueObject1s.Add(object2);
ctx.ValueObject1s.Add(object3);
ctx.SaveChanges();
*/
/*
foreach (var item in ctx.ValueObject1s)
{
    Console.WriteLine(item.Currency);
}
*/
/*
ValueObject1 object1 = new ValueObject1 { Name = "aaa", Currency = CurrencyName.CNY };
ValueObject1 object2 = new ValueObject1 { Name = "bbb", Currency = CurrencyName.USD };
ValueObject1 object3 = new ValueObject1 { Name = "ccc", Currency = CurrencyName.NZD };
ctx.ValueObject1s.Add(object1);
ctx.ValueObject1s.Add(object2);
ctx.ValueObject1s.Add(object3);
ctx.SaveChanges();

*/
//EFCore 值对象类型
/*
Shop shop1 = new Shop { Name = "rich的商店", Location = new Geo(66, 88) };
ctx.Shops.Add(shop1);
ctx.SaveChanges();
*/

/*
foreach (var item in ctx.Shops)
{
    Console.WriteLine(item.Location);
}
*/

/*
Blog b1 = new Blog { Title = new MultiLangString("你好", "Hello"), Body = new MultiLangString("ffff", "xxxx") };
Blog b2 = new Blog { Title = new MultiLangString("再见", "Bye"), Body = new MultiLangString("asdasd", "dddas") };
ctx.Blogs.Add(b1);
ctx.Blogs.Add(b2);
ctx.SaveChanges();
*/

//查询一条中文标题为你好的 blog
//var b = ctx.Blogs.First(t => t.Title.Chinese == "你好" && t.Title.English == "Hello");
//var b = ctx.Blogs.First(t => t.Title == new MultiLangString("你好", "Hello"));
//var b = ctx.Blogs.First(t => t.Title.Equals(new MultiLangString("你好", "Hello")));
/*
var b = ctx.Blogs.First(ExpressionHelper.MakeEqual((Blog t) => t.Title, new MultiLangString("你好", "Hello")));
Console.WriteLine(b.Id);
*/

//DDD聚合在.NET中的实现

//伪代码， 未实现数据库
/*
Order order = new Order();//聚合根
//调用聚合根的方法进行新增商品明细信息，而不是手动添加detail信息
order.AddDetail(new Merchan { Name = "商品", Price = 666 }, 5);
ctx.Orders.Add(order);
ctx.SaveChanges();
*/








//static void AA<T>(T t) where T : IAggreagateRoot
//{

//}