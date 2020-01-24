

namespace Sharing.Core.Entities
{
    public class Merchant : IMCode
    {
        public  long Id { get; set; }
        public  string MCode { get; set; }
        public  string BrandName { get; set; }
        public  string LogoUrl { get; set; }        
        public  string Address { get; set; }
    }
}
