

namespace Sharing.Core
{
    using Sharing.Core.Models;
    using System.Collections.Generic;

    public interface ISharingHostService:IMerchantService
    {        
        IList<MerchantDetails> MerchantDetails { get; }
		IList<ProductTreeNodeModel> Products { get;}

	}
}
