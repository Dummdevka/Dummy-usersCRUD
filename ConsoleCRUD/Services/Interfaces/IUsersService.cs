using System;
using ConsoleCRUD.Models;

namespace ConsoleCRUD.Services.Interfaces
{
	public interface IUsersService
	{
		public IEnumerable<User> listUsers();

		public User createUser(User newUser);

		public void deleteUser(int userId);

		public User updateUser(User userData);
	}
}

