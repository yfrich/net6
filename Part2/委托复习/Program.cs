using System;

namespace 委托复习
{
    class Program
    {
        static void Main(string[] args)
        {
            D1 d = F1;
            d();
            d = F2;
            d();
            D2 d2 = Add;
            Console.WriteLine(d2(5, 6));

            //无返回值
            Action a = F1;
            a();
            //有返回值 int
            Func<int, int, int> f = Add;
            Console.WriteLine(f(5, 6));
            //有返回值 string
            Func<int, int, string> f2 = F33;
            Console.WriteLine(f2(5, 6));
        }
        static void F1()
        {
            Console.WriteLine("我是F1");
        }
        static void F2()
        {
            Console.WriteLine("我是F2");
        }
        static int Add(int i1, int i2)
        {
            return i1 + i2;
        }
        static string F33(int i, int j)
        {
            return "XXX";
        }
    }
    delegate void D1();
    delegate int D2(int i1, int i2);
}
