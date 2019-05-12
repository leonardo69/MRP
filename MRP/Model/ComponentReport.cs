using System.Data;

namespace MRP.Model
{
    public class ComponentReport
    {
        public DataTable Results;

        public string NameComponent { get; set; }
        public string AvailableBalance { get; set; }
        public string LeadTime { get; set; }
        public string LotSize { get; set; }


    }
}
