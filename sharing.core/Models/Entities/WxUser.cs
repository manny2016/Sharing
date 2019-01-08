

namespace Sharing.Core.Entities
{
    public class WxUser : IWxUserKey, ISharedContext
    {
        public virtual long Id { get; set; }
        public virtual string UnionId { get; set; }
        public virtual string AppId { get; set; }
        public virtual string OpenId { get; set; }
        public virtual long? InvitedBy { get; set; }
        public virtual string Mobile { get; set; }
        public virtual AppTypes RegistrySource { get; set; }
        public virtual string NickName { get; set; }
        public virtual string Country { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual string AvatarUrl { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastActivityTime { get; set; }
    }
}
