

namespace Sharing.Core
{
    using Sharing.Core.Configuration;
    using System.Data.SqlClient;
    using MySql.Data.MySqlClient;
    using System;
    public static class DatabaseServerExtension
    {
        public static string GenerateConnectionString(this DatabaseServer server, string database)
        {
            switch (server.Type)
            {
                case DatabaseTypes.MySql:
                    return GenerateMySqlServerConnectionString(server, database);
                case DatabaseTypes.SqlServer:
                    return GenerateMsSqlServerConnectionString(server, database);
                default:
                    throw new NotSupportedException(server.Type.ToString());
            }
        }
        private static string GenerateMsSqlServerConnectionString(DatabaseServer server, string database)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = server.Server,
                InitialCatalog = database,
                UserID = server.UserId,
                Password = server.Password,
                Pooling = true,
                MinPoolSize = 50,
                /*
                 * https://docs.microsoft.com/en-us/azure/sql-database/sql-database-resource-limits#service-tiers-and-performance-levels
                 * eDTUs    50   100   125   200   250   300   400   500   800  1000  1200  1500  1600  2000  2500  3000  3500  4000
                 * Basic:     100   200   n/a   400   n/a   600   800   n/a  1600   n/a  2400   n/a  3200   n/a   n/a   n/a   n/a   n/a
                 * Standard:  100   200   n/a   400   n/a   600   800   n/a  1600   n/a  2400   n/a  3200  4000  5000  6000   n/a   n/a
                 * Premium:   n/a   n/a   200   n/a   400   n/a   n/a   800   n/a  1600   n/a  2400   n/a  3200  4000  4800  5600  6400
                 */
                MaxPoolSize = 200,
                //MultipleActiveResultSets = true,                     
                ConnectTimeout = 60 * 2,
                TrustServerCertificate = true,
            }.ToString();
        }
        private static string GenerateMySqlServerConnectionString(DatabaseServer server, string database)
        {
            return new MySqlConnectionStringBuilder()
            {
                UserID = server.UserId,
                Password = server.Password,
                Database = database,
                Server = server.Server,
                CharacterSet = "utf8",
                ConnectionTimeout = 60,
            }.ToString();
        }
        public static IDatabase GenerateDatabase(this DatabaseServer server, string database)
        {
            return Database.Generate(server.Type, server.GenerateConnectionString(database));
        }
    }
}
