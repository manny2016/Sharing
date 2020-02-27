using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Sharing.Core;
using Sharing.Core.Models;
using Sharing.WeChat.Models;

namespace Sharing.Agent.Delivery {
	public partial class FrmRedpack : Form {
		public FrmRedpack() {
			InitializeComponent();
			this.Load += FrmRedpack_Load;

		}

		private void FrmRedpack_Load(object sender, EventArgs e) {
			this.InitializeRewardLogging();
		}

		private void InitializeRewardLogging() {
			lv_RewardLogging.Items.Clear();
			var rewards = "https://www.yourc.club/api/sharing/GetRewardloggings"
				.GetUriJsonContent<JObject>((http) => {
					http.Method = "POST";
					http.ContentType = "application/json";
					using ( var stream = http.GetRequestStream() ) {
						var body = new {
							appid = "wx20da9548445a2ca7",
							state = 1
						}.SerializeToJson();
						var buffers = UTF8Encoding.UTF8.GetBytes(body);
						stream.Write(buffers, 0, buffers.Length);
						stream.Flush();
					}
					return http;
				})
				.TryGetValues<RewardLogging>("$.data");
			foreach ( var logging in rewards ) {
				var listItem = new ListViewItem(logging.NickName);
				listItem.Tag = logging;
				listItem.SubItems.Add(logging.RewardMoney.MoneyDisplay());
				listItem.SubItems.Add(logging.RealMoney.MoneyDisplay());
				listItem.SubItems.Add(logging.RelevantTradeId);
				listItem.SubItems.Add(logging.CreatedDateTime.ToDateTimeFromUnixStamp().ToString("yyyy-MM-dd HH:mm"));
				lv_RewardLogging.Items.Add(listItem);
			}
		}

		private void 全部发放ToolStripMenuItem_Click(object sender, EventArgs e) {

			var generator = new DefaultRandomGenerator();
			foreach ( var logging in this.lv_RewardLogging.SelectedItems ?? new ListView.SelectedListViewItemCollection(this.lv_RewardLogging) ) {
				var reward = (logging as ListViewItem).Tag as RewardLogging;
				var redpack = new Redpack(nonce_str: generator.Genernate(),
					mch_billno: reward.RelevantTradeId,
					mch_id: "1520961881",
					wxappid: "wx20da9548445a2ca7",
					send_name: "东坡区丽群奶茶店",
					re_openid: reward.OpenId,
					total_amount: reward.RewardMoney,
					total_num: 1,
					wishing: "感谢你的推荐,请收下这笔推广佣金!",
					client_ip: "49.76.219.137", act_name: "推广赚佣金", remark: reward.Description);
				SendLuckMoney("EA62B75D5D3941C3A632B8F18C7B3575", redpack);
			}

		}
		private RedpackResponse SendLuckMoney(string payKey, Redpack redpack) {
			//证书下载 地址  https://kf.qq.com/faq/180824JvUZ3i180824YvMNJj.html
			//证书下载 地址 https://kf.qq.com/faq/161222NneAJf161222U7fARv.html
			var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
			var sign = string.Concat(redpack.PrepareSign(), $"&key={payKey}").MakeSign(WxPayData.SIGN_TYPE_MD5, payKey);
			redpack.SetSign(sign);
			var xml = redpack.SerializeToXml();
			return url.GetUriContentDirectly((http) => {
				http.Method = "POST";
				http.ContentType = "text/xml";
				var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly | OpenFlags.MaxAllowed);
				var cert = store.Certificates.Find(X509FindType.FindByIssuerName, "MmpaymchCA", false);
				http.ClientCertificates.Add(cert[0]);
				using ( var stream = http.GetRequestStream() ) {
					var body = redpack.SerializeToXml();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			}).DeserializeFromXml<RedpackResponse>();
		}
	}
}
