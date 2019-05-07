﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Assembly Assembly { get; set; }
        public int WeekNumber { get; set; }
        public int Count { get; set; }
    }
}
