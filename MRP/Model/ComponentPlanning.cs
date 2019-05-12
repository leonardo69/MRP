using System.Collections.Generic;

namespace MRP.Model
{
    public class ComponentPlanning
    {
        public string ComponentName { get; set;}

        public string LotSize { get; set;}

        public int LeadTime { get; set; }

        public int StartAvailableBalance { get; set;}

        public List<WeekPlan> Weeks { get; set; }

        public ComponentPlanning Parent { get; set; }

        public List<ComponentPlanning> Children { get; set; }

        public bool IsMain { get; set;}


        public ComponentPlanning(string componentName)
        {
            Weeks = new List<WeekPlan>();
            Children = new List<ComponentPlanning>();
            for (var i = 0; i < 9; i++)
            { Weeks.Add(new WeekPlan()); }

            ComponentName = componentName;
        }

        public void SetupGrossRequirements()
        {
            for (var i = 0; i < 9; i++)
            {
                Weeks[i].GrossRequirements = Parent.Weeks[i].PlannedOrderReleases;
            }
        }


        public void MakeCalculation()
        {
            if (StartAvailableBalance >= Weeks[0].GrossRequirements)
            {
                Weeks[0].AvailableBalance = StartAvailableBalance - Weeks[0].GrossRequirements;
            }else
            {
                Weeks[0].PlannedOrderReceipts = Weeks[0].GrossRequirements - StartAvailableBalance;
                Weeks[0].AvailableBalance = 0;
            }

            for(var i=1;i<9;i++)
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

            for (var i=0; i< 9;i++)
            {
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
