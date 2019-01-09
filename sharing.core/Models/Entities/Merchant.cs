

namespace Sharing.Core.Entities
{
    public class Merchant : IMCode
    {
        public virtual long Id { get; set; }
        public virtual string MCode { get; set; }
        public virtual string BrandName { get; set; }
        public virtual string LogoUrl { get; set; }        
        public virtual string Address { get; set; }
    }
}
