
namespace Sharing.Core.Entities
{
    public class WxUserCard
    {
        public  long WxUserId { get; set; }

        public  string AppId { get; set; }

        public  string OpenId { get; set; }

        public  string CardId { get; set; }

        public  string UserCode { get; set; }

        public  int Integral { get; set; }

        public  int Money { get; set; }

        public  int RewardMoney { get; set; }

        public  long LastTradeId { get; set; }

        public  WxCardStates State { get; set; }
        public  long CreatedTime { get; set; }
        public  long LastActivityTime { get; set; }
    }
}
