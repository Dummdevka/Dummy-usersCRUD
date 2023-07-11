using System;
using System.Reflection;
using ConsoleCRUD.Models;
using ConsoleCRUD.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleCRUD.Services.Implementations
{
	public class ConsoleService
	{
		protected readonly IConfiguration __config;
		protected readonly IUsersService usersService;
		protected readonly ILogger<ConsoleService> __logger;

		public ConsoleService(IConfiguration config, IUsersService usersService, ILogger<ConsoleService> logger)
		{
			__config = config;
			__logger = logger;
			this.usersService = usersService;
		}

		public void Run() {
			List<char> commands = new List<char> { 'L', 'C', 'D', 'U' };
			char input = 'A';
			while (!commands.Contains(input)) {
				Console.WriteLine("Hello! Do you want to use this marvelous CRUD application?");
				Console.WriteLine("Type \"L\" for listing users, \"C\" for creating, \"D\" for deleting a user, \"U\" for updating");
				input = Console.ReadLine()[0];
			}
			try {
				switch (input) {
					case 'L':
						listUsers();
						break;
					case 'C':
						createUser();
						break;
					case 'D':
						deleteUser();
						break;
					case 'U':
						updateUser();
						break;

				}
			} catch (SqlException e) {
				//ADD LOGGING
				__logger.LogError("SQL error");
				Console.ReadLine();
			}
			
		}

		protected void listUsers() {
			Console.WriteLine("List users!");
			IEnumerable<User> users = usersService.listUsers();
			foreach (User user in users) {
				Console.WriteLine($"ID: {user.Id} Name: {user.Name} Email: {user.Email} Password: {user.Password}");
			}
			
			Run();
		}
		protected void createUser() {
			
			User user = new User();
			while (string.IsNullOrEmpty(user.Name)) {
				Console.WriteLine("Enter name:");
				user.Name = Console.ReadLine();
			}
			while (string.IsNullOrEmpty(user.Email)) {
				Console.WriteLine("Enter email:");
				user.Email = Console.ReadLine();
			}
			while (string.IsNullOrEmpty(user.Password)) {
				Console.WriteLine("Enter password:");
				user.Password = Console.ReadLine();
			}

			usersService.createUser(user);

			listUsers();
		}
		protected void deleteUser() {
			int id = -1;
			while (id == -1) {
				Console.WriteLine("Enter user id:");
				id = Convert.ToInt32(Console.ReadLine());
			}
			usersService.deleteUser(id);
			listUsers();
			
		}
		protected void updateUser() {
	
			User user = new User();
			user.Id = -1;
			while (user.Id == -1) {
				Console.WriteLine("Enter user id:");
				user.Id = Convert.ToInt32(Console.ReadLine());
			}
			while (string.IsNullOrEmpty(user.Name)) {
				Console.WriteLine("Enter name:");
				user.Name = Console.ReadLine();
			}
			while (string.IsNullOrEmpty(user.Email)) {
				Console.WriteLine("Enter email:");
				user.Email = Console.ReadLine();
			}
			while (string.IsNullOrEmpty(user.Password)) {
				Console.WriteLine("Enter password:");
				user.Password = Console.ReadLine();
			}
			usersService.updateUser(user);
			listUsers();
		}
	}
}

