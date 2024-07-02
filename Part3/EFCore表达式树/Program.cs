using ExpressionTreeToString;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZSpitz.Util;
using static System.Linq.Expressions.Expression;

namespace EFCore表达式树
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                //IQueryable 复用 动态构建。
                IQueryable<BookE> q1 = db.BookEs.Where(t => t.Price > 1);
                q1 = q1.Where(t => t.Title == "bzy");
                foreach (var item in q1)
                {
                    Console.WriteLine($"{item.ToString()}");
                }
                */

                /*
                //var q1 = await QueryDyamic(null, 1, 200, 1);
                var q1 = await QueryDyamic("bzy", 1, 200, 2);
                foreach (var item in q1)
                {
                    Console.WriteLine($"{item.ToString()}");
                }
                */
                //使用 System.Linq.Dynamic.Core
                db.BookEs.Where("Price>=3 and Price<=60").ToArray();
            }
        }

        static Task<BookE[]> QueryDyamic(string title, double? lowPrice, double? upPrice, int orderType)
        {
            using (MyDbContext db = new MyDbContext())
            {
                IQueryable<BookE> books = db.BookEs;
                if (!string.IsNullOrEmpty(title))
                {
                    books = books.Where(t => t.Title == title);
                }
                if (lowPrice != null)
                {
                    books = books.Where(t => t.Price > lowPrice);
                }
                if (upPrice != null)
                {
                    books = books.Where(t => t.Price < upPrice);
                }
                switch (orderType)
                {
                    case 1:
                        books = books.OrderByDescending(t => t.Price);
                        break;
                    case 2:
                        books = books.OrderBy(t => t.Price);
                        break;
                }
                return books.ToArrayAsync();
            }
        }
        static void MainTrtE1(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                var books = db.BookEs.Select(t => new { t.AuthorName, t.Price });
                foreach (var item in books)
                {
                    Console.WriteLine($"{item.AuthorName},{item.Price}");
                }
                */

                //通过数组的方式动态创建列
                /*
                var books = db.BookEs.Select(t => new object[] { t.Title, t.Price });
                foreach (var item in books)
                {
                    Console.WriteLine($"{item[0]},{item[1]}");
                }
                */
                //通过表达式树实现动态查询列

                var items = QueryProperNames<BookE>("Id", "Price");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item[0]},{item[1]}");
                }
            }
        }
        /// <summary>
        /// 通过表达式树实现动态列查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properNames"></param>
        /// <returns></returns>
        static IEnumerable<object[]> QueryProperNames<T>(params string[] properNames) where T : class
        {
            //
            var p = Parameter(typeof(T));
            List<Expression> propExprList = new List<Expression>();
            foreach (var item in properNames)
            {
                Expression exProp = Convert(MakeMemberAccess(p, typeof(T).GetProperty(item)), typeof(object));
                propExprList.Add(exProp);
            }
            var newArrayExpr = NewArrayInit(typeof(object), propExprList.ToArray());
            var selectExpr = Lambda<Func<T, object[]>>(newArrayExpr, p);
            using (MyDbContext db = new MyDbContext())
            {
                return db.Set<T>().Select(selectExpr).ToArray();
            }

        }
        static void MainTryE(string[] args)
        {
            using (MyDbContext db = new MyDbContext())
            {
                //运行时动态创建不同的表达式树的结构
                var books = QueryBooks("AuthorName", "bzy");
                foreach (var item in books)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
        static IEnumerable<BookE> QueryBooks(string propertyName, object value)
        {
            using (MyDbContext db = new MyDbContext())
            {
                /*
                Expression<Func<BookE, bool>> e1 = t => t.AuthorName == "bzy";
                return db.BookEs.Where(e1).ToArray();
                */
                //动态获取类型
                Type tp = typeof(BookE).GetProperty(propertyName).PropertyType;
                Expression<Func<BookE, bool>> e1;
                var t = Parameter(typeof(BookE), "t");
                var propertyExpr = MakeMemberAccess(t, typeof(BookE).GetProperty(propertyName));
                var constVauleExpr = Constant(System.Convert.ChangeType(value, tp));
                Expression body;
                if (tp.IsPrimitive)//原始类型
                {
                    //e1 = Lambda<Func<BookE, bool>>(
                    //    Equal(propertyExpr, constVauleExpr),
                    //    t);
                    body = Equal(propertyExpr, constVauleExpr);
                }
                else
                {
                    //非原始类型 字符串~~
                    body = MakeBinary(ExpressionType.Equal,
                        propertyExpr,
                        constVauleExpr,
                        false,
                        typeof(string).GetMethod("op_Equality")
                        );
                    //e1 = Lambda<Func<BookE, bool>>(
                    //     MakeBinary(ExpressionType.Equal,
                    //    propertyExpr,
                    //    constVauleExpr,
                    //    false,
                    //    typeof(string).GetMethod("op_Equality")
                    //    ),
                    //    t);
                }
                e1 = Lambda<Func<BookE, bool>>(body, t);
                return db.BookEs.Where(e1).ToArray();
            }
        }

        static void MainTree2(string[] args)
        {
            //工厂方式创建树
            /*
            Expression<Func<BookE, bool>> e1 = t => t.Price > 5;
            Expression<Func<BookE, BookE, double>> e2 = (t1, t2) => t1.Price + t2.Price;

            var t = Parameter(typeof(BookE), "t");

            var expr = Lambda<Func<BookE, bool>>(
                GreaterThan(
                    MakeMemberAccess(t,
                        typeof(BookE).GetProperty("Price")
                    ),
                    Constant(5.0)
                ),
                t
            );
            using (MyDbContext db = new MyDbContext())
            {
                db.BookEs.Where(expr).ToArray();
            }
            */

            Console.WriteLine($"请选择输出的筛选访视：1：大于 2：小于");
            string s = Console.ReadLine();
            var t = Parameter(typeof(BookE), "t");

            BinaryExpression exprCompare;
            var exprPrice = MakeMemberAccess(t,
                      typeof(BookE).GetProperty("Price")
                  );
            var const5 = Constant(5.0);
            if (s == "1")
            {
                exprCompare = GreaterThan(
                  exprPrice,
                   const5
               );
            }
            else
            {
                exprCompare = LessThan(
                  exprPrice,
                  const5
              );
            }
            var expr = Lambda<Func<BookE, bool>>(exprCompare, t);
            using (MyDbContext db = new MyDbContext())
            {
                db.BookEs.Where(expr).ToArray();
            }

        }
        static void MainTree(string[] args)
        {
            //注意编写EF数据过滤方式。 一定要加 Expression 否则会全表查询
            /*
            using (MyDbContext db = new MyDbContext())
            {
                var t = db.BookEs.Where(GetPrice);
                if (t != null && t.Count() > 0)
                {

                }
            }
            */

            Expression<Func<BookE, bool>> e1 = t => t.Price > 5;
            Expression<Func<BookE, BookE, double>> e2 = (t1, t2) => t1.Price + t2.Price;

            Console.WriteLine(e1.ToString("Factory methods", "C#"));



            //动态构建的作用
            /*
            Console.WriteLine($"请选择输出的筛选访视：1：大于 2：小于");
            string s = Console.ReadLine();
            //动态创建表达式树
            //实体树
            ParameterExpression parameterB = Expression.Parameter(typeof(BookE), "b");
            //常量树
            ConstantExpression constant5 = Expression.Constant(5.0, typeof(double));
            //对象树
            MemberExpression memberPrice = Expression.MakeMemberAccess(parameterB, typeof(BookE).GetProperty("Price"));
            //二元比较树主体
            BinaryExpression binaryExpressionCompare;
            if (s == "1")
            {
                binaryExpressionCompare = Expression.GreaterThan(memberPrice, constant5);
            }
            else
            {
                binaryExpressionCompare = Expression.LessThan(memberPrice, constant5);

            }
            //lambda表达式树
            Expression<Func<BookE, bool>> lambdaExpression = Expression.Lambda<Func<BookE, bool>>(binaryExpressionCompare, parameterB);


            using (MyDbContext db = new MyDbContext())
            {
                db.BookEs.Where(lambdaExpression).ToArray();
            }
            */
            /*

            //Expression<Func<BookE, bool>> e1 = t => t.Price > 5;
            //Expression<Func<BookE, bool>> e1 = t => { return t.Price > 5};
            Expression<Func<BookE, BookE, double>> e2 = (t1, t2) => t1.Price + t2.Price;

            //动态创建表达式树
            //实体树
            ParameterExpression parameterB = Expression.Parameter(typeof(BookE), "b");
            //常量树
            //ConstantExpression constant5 = Expression.Constant(5);
            //ConstantExpression constant5 = Expression.Constant(5, typeof(double));
            ConstantExpression constant5 = Expression.Constant(5.0, typeof(double));
            //对象树
            MemberExpression memberPrice = Expression.MakeMemberAccess(parameterB, typeof(BookE).GetProperty("Price"));
            //二元比较树主体
            BinaryExpression binaryExpressionThan = Expression.GreaterThan(memberPrice, constant5);
            //lambda表达式树
            Expression<Func<BookE, bool>> lambdaExpression = Expression.Lambda<Func<BookE, bool>>(binaryExpressionThan, parameterB);


            using (MyDbContext db = new MyDbContext())
            {
                db.BookEs.Where(lambdaExpression).ToArray();
            }
            */

            /*
            Console.WriteLine(e1.ToString("Object notation", "C#"));

            Func<BookE, bool> f1 = t => { Console.WriteLine("我来了"); return t.Price > 5; };
            Func<BookE, BookE, double> f2 = (t1, t2) => t1.Price + t2.Price;
            */
            /*
            using (MyDbContext db = new MyDbContext())
            {
                db.BookEs.Where(f1).ToArray();
            }
            */

        }
        static bool GetPrice(BookE b)
        {
            if (b.Price > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
