

namespace Sharing.Core.Entities
{
    public class Product
    {
        public virtual long Id { get; set; }
        public virtual long MchId { get; set; }
        public virtual long CategoryId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Price { get; set; }
        public virtual int SalesVol { get; set; }
        public virtual int SortNo { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string Description { get; set; }
    }
}
