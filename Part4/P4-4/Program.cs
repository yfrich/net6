using P4_4;

/*
Dog d1 = new Dog(1, "BZY");
Dog d2 = new Dog(1, "BZY");
Console.WriteLine(d1.ToString());
Console.WriteLine(d1 == d2);
Console.WriteLine(d1.Equals(d2));
//d1.Name = "1";
*/

/*
Person p1 = new Person(1, "BZY", 18);
Person p2 = new Person(1, "BZY", 18);
Person p3 = new Person(1, "暴走鱼", 18);
Console.WriteLine(p1.ToString());
Console.WriteLine(p2 == p3);
Console.WriteLine(p1 == p2);
Console.WriteLine(Object.ReferenceEquals(p1, p2));
Console.WriteLine(p1.Name);

*/
/*
Student s3 = new Student(1, "as", "das");
Student s1 = new Student(1, "SS");
s1.NickName = "5";
Student s2 = new Student(1, "SS");
s2.NickName = "hh";
Console.WriteLine(s1 == s2);
s2.NickName = "5";
Console.WriteLine(s1 == s2);
Console.WriteLine(s1.ToString());

Cat c1 = new Cat();
c1.Name = "tom";
c1.Id = 5;
Console.WriteLine(c1.ToString());

*/
Person p1 = new Person(3, "bzy", 18);
Person p2 = p1;
Console.WriteLine(Object.ReferenceEquals(p1, p2));
Person p3 = new(p1.Id, p1.Name, p1.Age);
Console.WriteLine(Object.ReferenceEquals(p1, p3));
Person p4 = p1 with { };//创建副本，不是同一个对象 但是内容完全一样。
Console.WriteLine(p4.ToString());
Console.WriteLine(p4 == p1);
Console.WriteLine(Object.ReferenceEquals(p1, p4));
Person p5 = p1 with { Age = 5 };
Console.WriteLine(p5.ToString());
Console.WriteLine(p1 == p5);

//非record 无法复制
//Dog dd1 = new Dog(1,"ss");
//Dog dd2 = dd1 with { };