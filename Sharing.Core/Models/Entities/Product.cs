

namespace Sharing.Core.Entities
{
    public class Product
    {
        public  long Id { get; set; }
        public  long MerchantId { get; set; }
        public  long CategoryId { get; set; }
        public  string Name { get; set; }
        public  int Price { get; set; }
        public  int SalesVol { get; set; }
        public  int SortNo { get; set; }
        public  bool Enabled { get; set; }
        public  string Description { get; set; }
    }
}
