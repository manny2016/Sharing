

namespace Sharing.Core
{
    using Sharing.Core.Entities;
    using Sharing.Core.Models;
    using System.Collections.Generic;
    public interface IMerchantService
    {
        IEnumerable<ProductModel> GetHotSaleProducts(long mchid, int top = 10);
        ProductTreeNodeModel[] GetProductTree(long mchid);
    }
}
