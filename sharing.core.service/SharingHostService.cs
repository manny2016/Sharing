
namespace Sharing.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Sharing.Core.Models;
    using System.Linq;

    public class SharingHostService : ISharingHostService
    {
        public SharingHostService()
        {
            Initialize();
        }
        public IList<MerchantDetails> MerchantDetails
        {
            get
            {
                return _MerchantDetails.Value;
            }
        }
        private static Lazy<MerchantDetails[]> _MerchantDetails;

        public T GetService<T>() where T : class
        {
            throw new NotImplementedException();
        }
        private void Initialize()
        {
            _MerchantDetails = new Lazy<MerchantDetails[]>(InitializeMerchantDetails);
        }
        private MerchantDetails[] InitializeMerchantDetails()
        {
            var queryString = @"
SELECT  
merchant.Id,
merchant.MCode,
merchant.BrandName,
(
	SELECT CONCAT(
    '[',
		GROUP_CONCAT(JSON_OBJECT('appid',wxapp.AppId,'OriginalId',wxapp.OriginalId,'secret',wxapp.Secret,'appType',wxapp.`AppType`)),
    ']') FROM  
    `sharing_mwechatapp` AS wxapp WHERE wxapp.MerchantId = merchant.Id
) AS WxApps
FROM `sharing_merchant` AS merchant;
";
            using (var database = SharingConfigurations.GenerateDatabase(false))
            {
                return database.SqlQuery<MerchantDetails>(queryString).ToArray();
            }

        }
    }
}
