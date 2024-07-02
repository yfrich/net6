using P4_3;
Student s1 = GetData();
Console.WriteLine(s1.Name.ToLower());
//Console.WriteLine(s1.PhoneNumber.ToLower());

//手动消除Null警告
Console.WriteLine(s1.PhoneNumber!.ToLower());

/*
if (s1.PhoneNumber == null)
{
    Console.WriteLine("没有手机");
}
else
{
    Console.WriteLine(s1.PhoneNumber.ToLower());
}
*/


static Student GetData()
{
    Student student = new Student("BZY");
    return student;
}