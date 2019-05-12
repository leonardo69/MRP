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
