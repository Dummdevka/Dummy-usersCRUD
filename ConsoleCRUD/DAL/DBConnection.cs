using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleCRUD.DAL
{
	public class DBConnection : IDBConnection
	{
		private readonly IConfiguration __config;
		public DBConnection(IConfiguration config)
		{
			__config = config;
		}

		public SqlConnection getConnection() {
			string connString = __config.GetConnectionString("Default");
			SqlConnection connection = new SqlConnection(connString);
			return connection;
		}
	}
}

