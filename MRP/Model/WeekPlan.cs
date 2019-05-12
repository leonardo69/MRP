namespace MRP.Model
{
    public class WeekPlan
    {
        public int NumberWeek { get; set; }

        public int GrossRequirements { get; set; }
        public int PlannedOrderReceipts { get; set; }
        public int AvailableBalance { get; set; }
        public int PlannedOrderReleases { get; set; }

    }
}
