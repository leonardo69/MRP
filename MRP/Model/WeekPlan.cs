namespace MRP.Model
{
    class WeekPlan
    {
        public int NumberWeek { get; set; }

        //детали
        public int GrossRequirements { get; set; }
        public int PlannedOrderReceipts { get; set; }
        public int AvailableBalance { get; set; }
        public int PlannedOrderReleases { get; set; }

    }
}
