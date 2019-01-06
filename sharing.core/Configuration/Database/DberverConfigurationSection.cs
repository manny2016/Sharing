using System.Configuration;

namespace Sharing.Core.Configuration
{
    public class DberverConfigurationSection : ConfigurationSection
    {
        public const string SectionName = "dbConfigSection";
        private static DberverConfigurationSection instance = null;
        public static DberverConfigurationSection GetInstance()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None);
            return configuration.GetSection(SectionName)
                 as DberverConfigurationSection;

            
        }
        public static DberverConfigurationSection GetInstanceForTest()
        {            
            var configuration = ConfigurationManager.OpenMachineConfiguration();
            return configuration.GetSection(SectionName)
                 as DberverConfigurationSection;
        }

        [ConfigurationProperty("master")]
        public DatabaseServer MasterDatabaseServer
        {
            get
            {
                return (DatabaseServer)this["master"];
            }
            set
            {
                this["master"] = value;
            }
        }

        [ConfigurationProperty("slaves")]
        public DatabaseServers SlaveDatabaseServers
        {
            get
            {
                return (DatabaseServers)this["slaves"];
            }
            set
            {
                this["slaves"] = value;
            }
        }
    }
}
