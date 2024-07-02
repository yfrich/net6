using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore自引用的组织结构树
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                //数据插入；
                //指定父节点的情况下要子对象也要添加。
                //指定了子节点了就只需要添加父节点即可。
                /*
                OrgUnit t = new OrgUnit { Name = "我是董事长" };

                OrgUnit t1 = new OrgUnit { Name = "我是产品部" };
                t1.Parent = t;
                OrgUnit t11 = new OrgUnit { Name = "我是产品经理A" };
                OrgUnit t12 = new OrgUnit { Name = "我是产品经理B" };
                t11.Parent = t1;
                t12.Parent = t1;

                OrgUnit t2 = new OrgUnit { Name = "我是项目部" };
                t2.Parent = t;
                OrgUnit t21 = new OrgUnit { Name = "我是项目经理A" };
                OrgUnit t22 = new OrgUnit { Name = "我是项目经理B" };
                t21.Parent = t2;
                t22.Parent = t2;
                */

                //方式1
                /*
                t.Children.Add(t1);
                t.Children.Add(t2);
                t1.Children.Add(t11);
                t1.Children.Add(t12);
                t2.Children.Add(t21);
                t2.Children.Add(t22);
                db.OrgUnits.Add(t);
                */

                //方式2

                /*
                db.OrgUnits.Add(t);
                db.OrgUnits.Add(t1);
                db.OrgUnits.Add(t11);
                db.OrgUnits.Add(t12);
                db.OrgUnits.Add(t2);
                db.OrgUnits.Add(t21);
                db.OrgUnits.Add(t22);
                */


                //await db.SaveChangesAsync();
                var parent = db.OrgUnits.Single(t => t.Parent == null);//寻找根节点
                Console.WriteLine(parent.Name);
                PrintChildren(1, db, parent);

            }
        }
        /// <summary>
        /// 缩进打印Parent的所有子节点
        /// </summary>
        /// <param name="indentLevel"></param>
        /// <param name="myDbContext"></param>
        /// <param name="parent"></param>
        static void PrintChildren(int indentLevel, MyDbContext db, OrgUnit parent)
        {
            //PrintChildren();
            var children = db.OrgUnits.Where(t => t.Parent == parent);
            foreach (var item in children)
            {
                Console.WriteLine(new String('\t', indentLevel) + item.Name);
                PrintChildren(indentLevel + 1, db, item);
            }
        }
    }
}
