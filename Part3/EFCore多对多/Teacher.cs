using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore多对多
{
    class Teacher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
