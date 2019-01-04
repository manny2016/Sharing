using System;
using System.Configuration;

namespace Sharing.Core.Configuration
{
    public class DatabaseServer : ConfigurationElement
    {
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server
        {
            get { return (string)this["server"]; }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("isWriteOnly", IsRequired = true)]
        public bool IsWriteOnly
        {
            get { return (bool)this["isWriteOnly"]; }
            set { this["isWriteOnly"] = value; }
        }

        [ConfigurationProperty("userid", DefaultValue = "sharing-uat")]
        public string UserId
        {
            get { return (string)this["userid"]; }
            set { this["userid"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue = "Window2008")]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("dbtype", DefaultValue = DatabaseTypes.SqlServer)]
        public DatabaseTypes Type
        {
            get { return Enum.Parse<DatabaseTypes>(this["dbtype"].ToString()); }
            set { this["dbtype"] = value.ToString(); }
        }



    }
}
