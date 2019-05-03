using System.Collections.Generic;
using Kurswork.Model;

namespace MRP.Model
{
    class ComponentPlanning
    {
        public string NameComponent { get; set;}


        //MRP требование
        public string LotSize { get; set;}

        public int LeadTime { get; set; }

        public int StartAvailableBalance { get; set;}

        public List<WeekPlan> Weeks { get; set; }

        //структура
        public ComponentPlanning ComponentParent { get; set; }

        public bool IsMain { get; set;}


        public ComponentPlanning(string componentName)
        {
            Weeks = new List<WeekPlan>();
            for (int i = 0; i < 9; i++)
            { Weeks.Add(new WeekPlan()); }

            NameComponent = componentName;
        }


        internal void MakeCalculation()
        {

            //cчитаем первую неделю
            if (StartAvailableBalance >= Weeks[0].GrossRequirements)
            {
                Weeks[0].AvailableBalance = StartAvailableBalance - Weeks[0].GrossRequirements;
            }else
            {
                Weeks[0].PlannedOrderReceipts = Weeks[0].GrossRequirements - StartAvailableBalance;
                Weeks[0].AvailableBalance = 0;
            }

            //считаем последующие недели
            for(int i=1;i<9;i++)
            {
                if (Weeks[i-1].AvailableBalance >= Weeks[i].GrossRequirements)
                {
                    Weeks[i].AvailableBalance = Weeks[i-1].AvailableBalance-Weeks[i].GrossRequirements;
                }
                else
                {
                    Weeks[i].PlannedOrderReceipts = Weeks[i].GrossRequirements - Weeks[i - 1].AvailableBalance;
                    Weeks[i].AvailableBalance = 0;
                }

            }

            //делаем перенос времени выполнения заказа по MRP

            for (int i=0; i< 9;i++)
            {
                //проверить на вшивость
                if(Weeks[i].PlannedOrderReceipts!=0)
                {
                    if (i - LeadTime >= 0)
                    {
                        Weeks[i - LeadTime].PlannedOrderReleases = Weeks[i].PlannedOrderReceipts;
                    }
                }
            }

        
      

        }
    }
}
