﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore一对一
{
    class Delivery
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public Order Order { get; set; }
        public string Number { get; set; }
        public long OrderId { get; set; }
    }
}
