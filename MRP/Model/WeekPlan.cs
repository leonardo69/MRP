using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurswork.Model
{
    class WeekPlan
    {
        public int numberWeek { get; set; }

        //детали
        public int grossRequirements { get; set; }
        public int plannedOrderReceipts { get; set; }
        public int availableBalance { get; set; }
        public int plannedOrderReleases { get; set; }

    }
}
