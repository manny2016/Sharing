using Sharing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IMCardService
    {
        void Synchronous();

        int WriteIntoDatabase(IList<MCard> cards);
    }
}
