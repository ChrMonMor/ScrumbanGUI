using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

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