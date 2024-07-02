using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_3;

internal class Student
{
    //public string? Name { get; set; }
    //public string? PhoneNumber { get; set; }

    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public Student(string name)
    {
        this.Name = name;
    }
}
