using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Kurswork.Model
{
    class ComponentReport
    {
        public DataTable Results;

        public string NameComponent { get; set; }
        public string AvaibleBalance { get; set; }
        public string Leadtime { get; set; }
        public string LotSize { get; set; }


    }
}
