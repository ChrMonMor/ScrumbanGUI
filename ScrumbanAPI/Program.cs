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

        //the allmighty Query call
        app.MapGet("/All/", () =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Projects inner join Columns on Columns.Project_Id = Projects.Project_Id full outer join Items on Items.Column_Id = Columns.Column_Id order by Projects.Project_Id,  Columns.Column_Id, Items.Position"));
        });
        app.MapGet("/All/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Projects inner join Columns on Columns.Project_Id = Projects.Project_Id full outer join Items on Items.Column_Id = Columns.Column_Id where Projects.Project_Id = " + request.RouteValues["id"] +" order by Projects.Project_Id, Columns.Column_Id, Items.Position"));
        });

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
        app.MapGet("/Workers/Update/{id}/{name}", (HttpRequest request) =>
        {
            SqlUpdate($"update Workers set " + 
                $"Name = '{request.RouteValues["name"]}' " + 
                $"where Worker_Id = {request.RouteValues["id"]}");
        });
        app.MapGet("/Projects/Update/{id}/{name}", (HttpRequest request) =>
        {
            SqlUpdate($"update Projects set " + 
                $"Name = '{request.RouteValues["name"]}' " + 
                $"where Project_Id = {request.RouteValues["id"]}");
        });
        app.MapGet("/Items/Update/{id}/{cid}/{position}/{name}/{content}", (HttpRequest request) =>
        {
            SqlUpdate($"update Items set " +
                $"Column_Id = {request.RouteValues["cid"]}, " +
                $"Position = {request.RouteValues["position"]}, " + 
                $"Name = '{request.RouteValues["name"]}', " + 
                $"Content = '{request.RouteValues["content"]}'  " + 
                $"where Item_Id = {request.RouteValues["id"]}");
        });
        app.MapGet("/Columns/Update/{id}/{position}/{name}/{limit}", (HttpRequest request) => // note that columns cant change projects
        {
            SqlUpdate($"update Columns set " +
                $"Position = {request.RouteValues["position"]}, " +
                $"Name = '{request.RouteValues["name"]}', " +
                $"Limit =  {request.RouteValues["limit"]} " +
                $"where Column_Id = {request.RouteValues["id"]}");
        });

        // Queries for Creating a new row in tables
        app.MapGet("/Workers/Create/{name}", (HttpRequest request) =>
        {
            SqlUpdate($"insert into Workers (Name) values ('{request.RouteValues["name"]}')");
        });
        app.MapGet("/Projects/Create/{name}", (HttpRequest request) =>
        {
            SqlUpdate($"insert into Projects (Name) values ('{request.RouteValues["name"]}')");
        });
        app.MapGet("/Items/Create/{id}/{position}/{name}/{content}", (HttpRequest request) =>
        {
            SqlUpdate($"insert into Items (Column_Id, Position, Name, Content) values ({request.RouteValues["id"]}, {request.RouteValues["position"]}, '{request.RouteValues["name"]}', '{request.RouteValues["content"]}')");
        });
        app.MapGet("/Columns/Create/{id}/{position}/{name}/{limit}", (HttpRequest request) =>
        {
            SqlUpdate($"insert into Columns (Project_Id, Position, Name, Limit) values ({request.RouteValues["id"]}, {request.RouteValues["position"]}, '{request.RouteValues["name"]}', {request.RouteValues["limit"]})");
        });

        // Queries for deleting a single row in a table
        app.MapGet("/Workers/delete/{id}", (HttpRequest request) =>
        {
            SqlUpdate("delete from Workers where Worker_Id = " + request.RouteValues["id"]);
        });
        app.MapGet("/Projects/delete/{id}", (HttpRequest request) =>
        {
            SqlUpdate("delete from Projects where Project_Id = " + request.RouteValues["id"]);
        });
        app.MapGet("/Items/delete/{id}", (HttpRequest request) =>
        {
            SqlUpdate("EXEC DeleteItem @id = " + request.RouteValues["id"]);
        });
        app.MapGet("/Columns/delete/{id}", (HttpRequest request) =>
        {
            SqlUpdate("EXEC DeleteColumn @id = " + request.RouteValues["id"]);
        });

        // Query for getting columns for a project
        app.MapGet("/Projects/Columns/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Columns where Project_Id = " + request.RouteValues["id"] + "order by Position"));
        });
        app.MapGet("/Columns/Items/{id}", (HttpRequest request) =>
        {
            return JsonConvert.SerializeObject(SqlCall("select * from Items where Column_Id = " + request.RouteValues["id"] + " order by Position"));
        });
        app.MapGet("/Items/ItemsNewPositon/{id}/{cid}/{pos}", (HttpRequest request) =>
        {
            SqlUpdate($"EXEC ItemsNewPositon @Item_Id = {request.RouteValues["id"]}, @newCol = {request.RouteValues["cid"]}, @newPos = {request.RouteValues["pos"]}");
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