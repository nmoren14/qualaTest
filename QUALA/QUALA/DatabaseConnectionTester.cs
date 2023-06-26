using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QUALA.Data;

public class DatabaseConnectionTester
{
    private readonly IConfiguration _configuration;

    public DatabaseConnectionTester(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void TestConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(connectionString);

        try
        {
            using (var context = new ApplicationContext(optionsBuilder.Options))
            {
                context.Database.OpenConnection();
                Console.WriteLine("Connection to the database successful!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to the database:");
            Console.WriteLine(ex.Message);
        }
    }
}
