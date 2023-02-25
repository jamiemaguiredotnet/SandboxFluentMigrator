using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


internal class Program
{
    private static void Main(string[] args)
    {
        string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();
        string connectionString = configuration.GetConnectionString("Sandbox");


        var sc = new ServiceCollection()
        // Add common FluentMigrator services
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
        // Add SQL support to FluentMigrator
        .AddSqlServer()
        // Set the connection string
        .WithGlobalConnectionString(connectionString)
        // Define the assembly containing the migrations
        .ScanIn(typeof(SandboxMigrations.Sandbox).Assembly)
               .For.Migrations())
        // Enable logging to console in the FluentMigrator way
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        // Build the service provider
        .BuildServiceProvider(false);

        using (var sandpitScope = sc.CreateScope())
        {
            UpdateDatabase(sandpitScope.ServiceProvider);
        }
    }

    static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        // Execute the migrations
        if (runner.HasMigrationsToApplyUp())
        {
            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}