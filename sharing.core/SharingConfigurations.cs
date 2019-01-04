

namespace Sharing.Core
{
    using Sharing.Core.Configuration;
    using System;
    public static class SharingConfigurations
    {
        public static IDatabase GenerateDatabase(string database, bool isWrite)
        {
            var section = DberverConfigurationSection.GetInstanceForTest();
            if (isWrite)
            {
                return section.MasterDatabaseServer.GenerateDatabase(database);
            }
            else
            {
                var idx = 0;
                if (section.SlaveDatabaseServers.Count > 0)
                {
                    idx = DateTime.Now.Second % section.SlaveDatabaseServers.Count;
                }
                return section.SlaveDatabaseServers[idx].GenerateDatabase(database);
            }
        }
    }
}
