using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class SharingPrimaryKey : IEntityKey<long>
    {
        public virtual long Id { get; set; }
    }
}
