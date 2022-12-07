using System;
using System.Collections.Generic;
using System.Data.SqlClient;


internal class Program
{
    // tenperary placement 
    private static readonly string _con = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToemreDate;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        SqlConnection connection = new SqlConnection(_con);
        SqlCommand command = new SqlCommand("Select * from Vare", connection);

        app.MapGet("/workers/all", () =>
        {
            return "World";
        });

        app.MapGet("/projects/all", () =>
        {
            return "Hallo";
        });

        app.Run();
    }
}