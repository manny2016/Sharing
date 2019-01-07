using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IRandomGenerator
    {
        uint GetRandomUInt();
        string Genernate();
    }
}
