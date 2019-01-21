using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class SharedPyramid : ISharedPyramid
    {
        public long Id { get; set; }

        public int Level { get; set; }
        public long MchId { get; set; }
        public ISharedPyramid Parent { get; set; }
    }
}
