using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurswork.Model
{
    class ComponentPlanning
    {
        public string nameComponent { get; set;}


        //MRP требование
        public string lotSize { get; set;}

        public int leadTime { get; set; }

        public int startAvailableBalance { get; set;}

        public List<WeekPlan> weeks { get; set; }

        //структура
        public ComponentPlanning ComponentParent { get; set; }

        public bool IsMain { get; set;}


        public ComponentPlanning(string componentName)
        {
            weeks = new List<WeekPlan>();
            for (int i = 0; i < 9; i++)
            { weeks.Add(new WeekPlan()); }

            nameComponent = componentName;
        }


        internal void MakeCalculation()
        {

            //cчитаем первую неделю
            if (startAvailableBalance >= weeks[0].grossRequirements)
            {
                weeks[0].availableBalance = startAvailableBalance - weeks[0].grossRequirements;
            }else
            {
                weeks[0].plannedOrderReceipts = weeks[0].grossRequirements - startAvailableBalance;
                weeks[0].availableBalance = 0;
            }

            //считаем последующие недели
            for(int i=1;i<9;i++)
            {
                if (weeks[i-1].availableBalance >= weeks[i].grossRequirements)
                {
                    weeks[i].availableBalance = weeks[i-1].availableBalance-weeks[i].grossRequirements;
                }
                else
                {
                    weeks[i].plannedOrderReceipts = weeks[i].grossRequirements - weeks[i - 1].availableBalance;
                    weeks[i].availableBalance = 0;
                }

            }

            //делаем перенос времени выполнения заказа по MRP

            for (int i=0; i< 9;i++)
            {
                //проверить на вшивость
                if(weeks[i].plannedOrderReceipts!=0)
                {
                    if (i - leadTime >= 0)
                    {
                        weeks[i - leadTime].plannedOrderReleases = weeks[i].plannedOrderReceipts;
                    }
                }
            }

        
      

        }
    }
}
