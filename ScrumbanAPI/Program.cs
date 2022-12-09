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

        builder.Services.AddCors(p => p.AddPolicy("test", build =>
        {
            build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));

        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.


        app.UseCors("test");

        // Queries for get all data from a table
        app.MapGet("/Workers/", () =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Workers"));
        });
        app.MapGet("/Projects/", () =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Projects"));
        });
        app.MapGet("/Items/", () =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Items"));
        });
        app.MapGet("/Columns/", () =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Columns"));
        });

        // Queries for getting a row based on its id
        app.MapGet("/Workers/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Workers where Worker_Id = " + request.RouteValues["id"]));
        });
        app.MapGet("/Projects/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Projects where Project_Id = " + request.RouteValues["id"]));
        });
        app.MapGet("/Items/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Items where Item_Id = " + request.RouteValues["id"]));
        });
        app.MapGet("/Columns/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Columns where Column_Id = "+ request.RouteValues["id"]));
        });

        // Queries for Updating a row based on its id
        app.MapGet("/Workers/{id}/{name}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"update Workers set " +
                $"Name = {request.RouteValues["name"]} " +
                $"where Worker_Id = {request.RouteValues["id"]}"));
        });
        app.MapGet("/Projects/{id}/{name}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"update Projects set " +
                $"Name = {request.RouteValues["name"]} " +
                $"where Worker_Id = {request.RouteValues["id"]}"));
        });
        app.MapGet("/Items/{id}/cid/{position}/{name}/{content}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"update Items set " +
                $"Column = {request.RouteValues["cid"]}, " +
                $"Position = {request.RouteValues["name"]}, " +
                $"Name = {request.RouteValues["name"]}, " +
                $"Content = {request.RouteValues["content"]}  " +
                $"where Worker_Id = {request.RouteValues["id"]}"));
        });
        app.MapGet("/Columns/{id}/{}/{name}/{limit}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"update Columns set " +
                $"Name = {request.RouteValues["name"]}, " +
                $"Limit =  {request.RouteValues["limit"]}" +
                $"where Worker_Id = {request.RouteValues["id"]}"));
        });

        // Queries for Craeting new rows in tables
        app.MapGet("/Workers/{name}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"insert into Workers (Name) values ({request.RouteValues["name"]})"));
        });
        app.MapGet("/Projects/{name}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"insert into Projects (Name) values ({request.RouteValues["name"]})"));
        });
        app.MapGet("/Items/{id}/{position}/{name}/{content}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"insert into Workers (name) values ({request.RouteValues["name"]})"));
        });
        app.MapGet("/Columns/{id}/{name}/{limit}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall($"insert into Workers (name) values ({request.RouteValues["name"]})"));
        });



        // runs the app and needs to be at the end!!!!
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
    private static void SqlUpdate(string query)
    {
        SqlConnection connection = new SqlConnection(_con);
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}