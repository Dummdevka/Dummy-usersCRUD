using ConsoleCRUD.DAL;
using ConsoleCRUD.Services.Implementations;
using ConsoleCRUD.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

class Program
{
	protected static string rootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

	 static void Main() {
		var builder = new ConfigurationBuilder();

		setConfig(builder);

		Log.Logger = new LoggerConfiguration()
		   .ReadFrom.Configuration(builder.Build())
		   .Enrich.FromLogContext()
		   .WriteTo.Console()
		   .WriteTo.File(rootPath + "/logs-.txt", rollingInterval: RollingInterval.Day)
		   .CreateLogger();

		var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
		{
			services.AddSingleton<IDBConnection, DBConnection>();
			services.AddSingleton<IUsersService, UsersService>();
		}).UseSerilog().Build();

		ConsoleService svc = ActivatorUtilities.CreateInstance<ConsoleService>(host.Services);

		svc.Run();

	}

	static void setConfig(IConfigurationBuilder builder) {
		builder
			.SetBasePath(rootPath)
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables();
	}
}

