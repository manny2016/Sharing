

namespace Sharing.Core.Entities
{
    public class WxUser : IWxUserKey, ISharedContext
    {
        public  long Id { get; set; }
        public  long MchId { get; set; }
        public  string UnionId { get; set; }
        public  long? InvitedBy { get; set; }
        public  string Mobile { get; set; }
        public  AppTypes RegistrySource { get; set; }
        public  string NickName { get; set; }
        public  string Country { get; set; }
        public  string Province { get; set; }
        public  string City { get; set; }
        public  string AvatarUrl { get; set; }
        public  long CreatedTime { get; set; }
        public  long LastActivityTime { get; set; }

        public string AppId { get; set; }

        public string OpenId { get; set; }
    }
}
