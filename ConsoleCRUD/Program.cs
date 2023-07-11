using ConsoleCRUD.DAL;
using ConsoleCRUD.Services.Implementations;
using ConsoleCRUD.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
	public static void Main() {
		var builder = new ConfigurationBuilder();

		setConfig(builder);

		var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
		{
			services.AddSingleton<IDBConnection, DBConnection>();
			services.AddSingleton<IUsersService, UsersService>();
		}).Build();

		ConsoleService svc = ActivatorUtilities.CreateInstance<ConsoleService>(host.Services);

		svc.Run();

	}

	static void setConfig(IConfigurationBuilder builder) {
		string rootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
		builder
			.SetBasePath(rootPath)
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables();
	}
}

