using System;
using System.Collections.Generic;
using System.Text;


namespace Sharing.Core.Entities
{
    public class Category
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual long MchId { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string Description { get; set; }
        public virtual string JsonString { get; set; }
        
    }
}
