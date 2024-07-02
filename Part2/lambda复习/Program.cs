using System;

namespace lambda复习
{
    class Program
    {
        static void Main(string[] args)
        {
            //不带参数匿名方法
            Action f1 = delegate ()
              {
                  Console.WriteLine("我是AAA");
              };
            f1();
            //带参数的匿名方法
            Action<string, int> f2 = delegate (string n, int i)
              {
                  Console.WriteLine($"n={n},i={i}");
              };
            f2("bzy", 18);
            //匿名方法 带参数 带返回值
            Func<int, int, int> f3 = delegate (int i, int j)
                {
                    return i + j;
                };
            Console.WriteLine(f3(5, 3));

            //lambda表达式 => gosto语法
            Func<int, int, int> f4 = (int i, int j) =>
                {
                    return i + j;
                };
            Console.WriteLine(f4(3, 3));

            //可以省略参数类型 编译器可以推断出参数类型
            Func<int, int, int> f5 = (i, j) =>
            {
                return i + j;
            };
            Console.WriteLine(f5(3, 3));

            //如果方法没有返回值 且只有一行代码 可以省略{}

            Action f6 = () => Console.WriteLine("F6");
            f6();

            Action<string, int> f7 = (string n, int i) => Console.WriteLine($"n={n},i={i}");
            f7("bzy", 5);

            //如果有有返回值 且只有一行代码
            Func<int, int, int> f8 = (i, j) => i + j;
            Console.WriteLine(f8(1, 3));

            //如果只有一个参数 那么参数的 ()可以省略
            Action<int> f9 = s => Console.WriteLine(s);
            f9(5);

            Func<int, bool> f10 = delegate (int i)
                {
                    return i > 0;
                };
            Console.WriteLine(f10(3));

            Func<int, bool> f11 = i => i > 0;
            Console.WriteLine(f11(15));
            //理解委托：方法中定义无参委托。 
            Test("awsd", t =>
            {
                Console.WriteLine(t);
            }
            );
            //
            Test2("我是参数给", () => "我是func委托给");
        }
        static void Test(string t, Action<string> a)
        {
            //你外部给方法实现，我内部使用。具体实现啥，你决定，什么时候调用，我决定
            a(t + "asas");
        }
        static void Test2(string t, Func<string> a)
        {
            //你外部给我一个我要的类型，我内部要用，具体怎么给，你调用自己实现。
            Console.WriteLine(t + ":" + a());
        }
    }
}
