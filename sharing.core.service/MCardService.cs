
namespace Sharing.Core.Services {
	using Sharing.Core.Entities;
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Linq;
	using Microsoft.Extensions.DependencyInjection;
	using System.Threading.Tasks;
	using Sharing.Core.Models;
	using Sharing.Core;
	using System.Collections.Concurrent;
	using Newtonsoft.Json.Linq;
	using Newtonsoft.Json;
	public class MCardService : IMCardService {
		//private readonly IServiceProvider provider = SharingConfigurations
		//    .CreateServiceCollection()
		//    .BuildServiceProvider();
		private readonly IWeChatApi api;
		private readonly IDatabaseFactory databaseFactory;
		public MCardService(IWeChatApi api,IDatabaseFactory databaseFactory) {
			this.api = api;
			this.databaseFactory = databaseFactory;
		}
		public void Synchronous() {
			var cards = new ConcurrentDictionary<string, MCard>();
			Parallel.ForEach(this.GetWeChatOfficials(),
				new ParallelOptions() { MaxDegreeOfParallelism = 5 },
			(app) => {
				//Sync Merchant Member Card from WeChat Server             
				foreach ( var cardid in api.QueryMCard(app).CardIdList ) {
					var details = this.api.QueryMCardDetails(app, new MCardKey() { CardId = cardid }).ParseMCard(app.MerchantId);
					cards[cardid] = details;
				}

				//Sync User Member Card from WeChat Server.


			});
			this.WriteIntoDatabase(cards.Values.ToList());
		}
		const string SqlSyncMCard = @"
CREATE TEMPORARY TABLE `tmcards` (
  `MerchantId` bigint(20) NOT NULL,
  `CardId` varchar(32) COLLATE utf8_bin NOT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `TotalQuantity` int(11) DEFAULT NULL,
  `Discount` decimal(8,2) DEFAULT NULL,
  `Title` varchar(64) COLLATE utf8_bin DEFAULT NULL,
  `BrandName` varchar(64) COLLATE utf8_bin NOT NULL,
  `Prerogative` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `LogoUrl` varchar(200) COLLATE utf8_bin NOT NULL,
  `RawData` varchar(4000) COLLATE utf8_bin NOT NULL
) ENGINE=MEMORY DEFAULT CHARSET=utf8;
INSERT INTO tmcards
VALUES
{0};
INSERT INTO `sharing_mcard`(MerchantId, CardId, Quantity, TotalQuantity, Discount, Title, BrandName, Prerogative, LogoUrl, RawData)
    SELECT src.MerchantId,src.CardId,src.Quantity,src.TotalQuantity,src.Discount,src.Title,src.BrandName,src.Prerogative,src.LogoUrl,src.RawData FROM  tmcards AS src
   LEFT JOIN  `sharing_mcard` AS target
        ON src.CardId = target.CardId AND src.MerchantId = target.MerchantId
     ON DUPLICATE KEY UPDATE  Quantity = VALUES(Quantity), TotalQuantity= VALUES(TotalQuantity);
DROP TABLE `tmcards`;";
		public int WriteIntoDatabase(IList<MCard> cards) {
			if ( cards == null || cards.Count == 0 ) return 0;

			using ( var database = this.databaseFactory.GenerateDatabase(isWriteOnly: true) ) {
				return database.Execute(string.Format(SqlSyncMCard,
					string.Join(",", cards.Select(o => o.GenerateMySqlInsertValuesString()))));
			}
		}

		private IList<MWeChatApp> GetWeChatOfficials() {
			using ( var database = this.databaseFactory.GenerateDatabase(isWriteOnly: false) ) {
				var result = database.SqlQuery<MWeChatApp>("SELECT * FROM `sharing_mwechatapp` WHERE AppType=@appType",
					new { appType = AppTypes.Official.ToString() }
					)
					.ToList<MWeChatApp>();
				return result;
			}
		}


	}
}
