

namespace Sharing.Core
{
    using System.Collections.Generic;
    using System.Data;
    using Dapper;
    using System;
    public class Database : IDatabase
    {
        private IDbConnection connection;
        public static Database Generate(DatabaseTypes type, string connectionString)
        {
            switch (type)
            {
                case DatabaseTypes.MySql:
                    return new Database(new MySql.Data.MySqlClient.MySqlConnection(connectionString));
                case DatabaseTypes.SqlServer:
                    return new Database(new System.Data.SqlClient.SqlConnection(connectionString));
                default:
                    throw new NotSupportedException(type.ToString());
            }
        }
        private Database(IDbConnection connection)
        {
            this.connection = connection;
        }
        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Close();
            }
        }

        public IEnumerable<T> SqlQuery<T>(
            string queryString,
            object param = null)
        {
            return connection.Query<T>(queryString, param);
        }

        public T SqlQuerySingleOrDefault<T>(
            string queryString,
            object param = null)
        {

            return connection.QueryFirstOrDefault<T>(queryString, param);
        }

        public int Execute(string executeSql, object param = null)
        {

            return connection.Execute(executeSql, param);
        }
    }
}
