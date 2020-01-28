



namespace Sharing.Core {
	using System.Collections.Generic;
	using System;
	using System.Data;
	using Dapper;

	public interface IDatabase : IDisposable {

		IEnumerable<T> SqlQuery<T>(
		  string queryString,
		  object param = null);

		T SqlQuerySingleOrDefault<T>(
			string queryString,
			object param = null);

		T SqlQuerySingleOrDefaultTransaction<T>(
		  string queryString, object param = null);
		int Execute(string executeSql, object param = null);

		int Execute(string executeSql, DynamicParameters parameters, CommandType type);

		int Execute(string executeSql, DynamicParameters parameters, CommandType type, bool useTransaction);
		int Execute(string executeSql, IDbDataParameter[] parameters, CommandType commandType = CommandType.Text, int timeout = 5);
	}
}
