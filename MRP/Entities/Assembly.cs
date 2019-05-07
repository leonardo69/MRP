namespace MRP.Entities
{
    public class Assembly
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Component StartComponent { get; set; }
    }
}
