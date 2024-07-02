using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4_4;

internal class Dog
{
    //public int Id { get; set; }
    //public string Name { get; set; }
    //Init语法

    public int Id { get; set; }
    public string Name { get; init; }
    public Dog(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
    /*
    void AA()
    {
        this.Name = "";
    }
    */

    public override string ToString()
    {
        return $"Id={Id},Name={Name}";
    }
}
