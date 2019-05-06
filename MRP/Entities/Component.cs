using System.Collections.Generic;

namespace MRP.Entities
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int? SpecificationId { get; set; }
        public Component Parent { get; set; }
        public ICollection<Component> Childrens { get; set; }
    }
}
