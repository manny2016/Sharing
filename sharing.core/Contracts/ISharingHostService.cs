

namespace Sharing.Core
{
    using Sharing.Core.Models;
    using System.Collections.Generic;

    public interface ISharingHostService
    {

        T GetService<T>() where T : class;

        IList<MerchantDetails> MerchantDetails { get; }
    }
}
