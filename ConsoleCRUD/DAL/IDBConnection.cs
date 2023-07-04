using System;
using Microsoft.Data.SqlClient;

namespace ConsoleCRUD.DAL
{
	public interface IDBConnection
	{
		public SqlConnection getConnection();
	}
}

