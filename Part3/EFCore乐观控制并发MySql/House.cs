using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore悲观并发MySql
{
    class House
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
