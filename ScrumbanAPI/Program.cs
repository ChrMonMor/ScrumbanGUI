using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

internal class Program
{
    // tenperary placement 
    private static readonly string _con = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Scrumban;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.MapGet("/Workers/", () =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Workers"));
        });

        app.Run();
    }
    private static DataTable SqlCall(string query)
    {
        SqlConnection connection = new SqlConnection(_con);
        SqlCommand command = new SqlCommand(query, connection);
        DataTable data = new DataTable();
        connection.Open();
        SqlDataAdapter result = new SqlDataAdapter(command);
        connection.Close();
        result.Fill(data);

        return data;
    }
}