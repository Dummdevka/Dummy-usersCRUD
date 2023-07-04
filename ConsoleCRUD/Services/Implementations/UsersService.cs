using System;
using ConsoleCRUD.DAL;
using ConsoleCRUD.Models;
using ConsoleCRUD.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ConsoleCRUD.Services.Implementations
{
	public class UsersService : IUsersService
	{
		private readonly IDBConnection _conn;
		public UsersService(IDBConnection conn)
		{
			_conn = conn;
		}

		public IEnumerable<User> listUsers() {
			using (SqlConnection connection = _conn.getConnection()) {
				List<User> users = (List<User>)connection.Query<User>("select * from [User]");
				return users;
			}
			//return null;
		}

		public User createUser(User newUser) {
			using (SqlConnection connection = _conn.getConnection()) {
				connection.Query<User>("insert into [User] (Name, Email, Password) values (@name, @email, @password)",
					new {
						name = newUser.Name,
						email = newUser.Email,
						password = newUser.Password
					});
				return newUser;
			}
		}

		public void deleteUser(int userId) {
			using (SqlConnection connection = _conn.getConnection()) {
				connection.Query("delete from [User] where Id=@id", new {
					id = userId
				});
			}
		}

		public User updateUser(User userData) {
			using (SqlConnection connection = _conn.getConnection()) {
				connection.Query<User>("update [User] set Name=@name, Email=@email, Password=@password where id=@id",
					new {
						name = userData.Name,
						email = userData.Email,
						password = userData.Password,
						id = userData.Id
					});
				return userData;
			}
		}
	}
}

