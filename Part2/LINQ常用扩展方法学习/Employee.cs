using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ常用扩展方法学习
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public int Salary { get; set; }
        public override string ToString()
        {
            return $"Id={Id},Name={Name},Age={Age},Gender={Gender},Salary={Salary}";
        }
    }
}
