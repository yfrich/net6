using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTest2
{
    class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public double Height { get; set; }
        public double Weight { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
