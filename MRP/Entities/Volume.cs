using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Entities
{
    public class Volume
    {
        public int Id { get; set; }
        public Component Component { get; set; }
        public string Count { get; set; }
    }
}
