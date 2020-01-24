using System;
using System.Collections.Generic;
using System.Text;


namespace Sharing.Core.Entities
{
    public class Category
    {
        public  long Id { get; set; }
        public  string Name { get; set; }
        public  long MerchantId { get; set; }
        public  bool Enabled { get; set; }
        public  string Description { get; set; }
        public  string JsonString { get; set; }
        
    }
}
