

namespace Sharing.Core {
	using System.Collections.Generic;
	using System.Data;
	using Dapper;
	using System;
	using System.Data.Common;
	public class Database : IDatabase {
		private IDbConnection connection;
		public static Database Generate(DatabaseTypes type, string connectionString) {
			switch ( type ) {
				case DatabaseTypes.MySql:
					return new Database(new MySql.Data.MySqlClient.MySqlConnection(connectionString));
				case DatabaseTypes.SqlServer:
					return new Database(new System.Data.SqlClient.SqlConnection(connectionString));
				default:
					throw new NotSupportedException(type.ToString());
			}
		}
		private Database(IDbConnection connection) {
			this.connection = connection;
		}
		public void Dispose() {
			if ( connection != null ) {
				if ( connection.State != ConnectionState.Open )
					connection.Close();
			}
		}

		public IEnumerable<T> SqlQuery<T>(
			string queryString,
			object param = null) {
			return connection.Query<T>(queryString, param);
		}

		public T SqlQuerySingleOrDefault<T>(
			string queryString,
			object param = null) {
			return connection.QueryFirstOrDefault<T>(queryString, param);
		}




		public T SqlQuerySingleOrDefaultTransaction<T>(string queryString, object param = null) {
			IDbTransaction transcation = null;
			try {
				if ( connection.State != ConnectionState.Open )
					connection.Open();
				transcation = connection.BeginTransaction();
				var result = connection.QuerySingleOrDefault<T>(queryString, param, transcation);
				transcation.Commit();
				return result;
			} catch ( Exception ex ) {
				transcation.Rollback();
				throw ex;
			}
		}
		public int Execute(string executeSql, object parameters) {
			return connection.Execute(executeSql, parameters);
		}

		public int Execute(string executeSql, DynamicParameters parameters, CommandType type) {
			return Execute(executeSql, parameters, type, false);
		}
		public int Execute(string executeSql, DynamicParameters parameters, CommandType type, bool useTransaction) {

			IDbTransaction transcation = null;
			try {
				if ( connection.State != ConnectionState.Open )
					connection.Open();
				transcation = useTransaction ? connection.BeginTransaction() : null;
				var result = connection.Execute(executeSql, parameters, transcation, null, type);
				if ( transcation != null ) {
					transcation.Commit();
				}
				return result;
			} catch ( Exception ex ) {
				if ( transcation != null )
					transcation.Rollback();
				throw ex;
			}
		}

		public int Execute(string commandText, IDbDataParameter[] parameters = null, CommandType commandType = CommandType.Text, int timeout = 5) {
			try {
				if ( connection.State != ConnectionState.Open )
					connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = commandText;
				command.CommandType = commandType;
				foreach ( var parameter in parameters ?? new IDbDataParameter[] { } ) {
					command.Parameters.Add(parameter);
				}
				return command.ExecuteNonQuery();
		} catch ( Exception ex ) {

				throw ex;
			}
		}
	}
}
