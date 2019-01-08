
namespace Sharing.Core.Entities
{
    public class WxUserCard 
    {
        public virtual long WxUserId { get; set; }
        public virtual string CardId { get; set; }
        public virtual string UserCode { get; set; }
        public virtual int Integral { get; set; }
        public virtual int Money { get; set; }
        public virtual int RewardMoney { get; set; }
        public virtual long LastTradeId { get; set; }
        public virtual WxCardStates State { get; set; }
        public virtual long CreatedTime { get; set; }
        public virtual long LastActivityTime { get; set; }
    }
}
