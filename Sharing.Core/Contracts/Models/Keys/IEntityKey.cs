using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IEntityKey<T>
    {
        T Id { get; set; }
    }
}
