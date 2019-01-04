using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Sharing.Core.Configuration
{
    public class DatabaseServers : ConfigurationElementCollection
    {
        public DatabaseServer this[int idx]
        {
            get
            {
                return BaseGet(idx) as DatabaseServer;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseServer();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseServer)element).Server;
        }
    }
}
