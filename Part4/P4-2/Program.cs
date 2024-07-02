
using var fs = File.OpenWrite("f:/1.txt");
using var write = new StreamWriter(fs);

/*
//传统解决方式
using (var fs = File.OpenWrite("f:/1.txt"))
using (var write = new StreamWriter(fs))
{
    write.WriteLine("我是哈哈哈");
}
string s = File.ReadAllText("f:/1.txt");
Console.WriteLine(s);
*/

/*
P4_2.Class1 c1 = null;
//新版本方式 手动添加作用域
{
    using var fs = File.OpenWrite("f:/1.txt");
    using var write = new StreamWriter(fs);
    write.WriteLine("我是哈哈哈");
}

string s = File.ReadAllText("f:/1.txt");
Console.WriteLine(s);
*/

/*
using P4_2;

Console.WriteLine("Hello, World!");
A();
Console.WriteLine("方法执行完成");

static void A()
{
    //using MyFile fi = new MyFile();
    for (int i = 0; i < 2; i++)
    {
        using MyFile fi = new MyFile();
        Console.WriteLine($"i={i}");
    }
    Console.WriteLine("循环执行完成");
}
*/
