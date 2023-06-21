using MySqlConnector;
using Pomelo.EntityFrameworkCore;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabase;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var sqlVersion = new MySqlServerVersion(new Version(8, 0, 31));
        string? connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<MovieContext>(x =>
        x.UseMySql(connectionString, sqlVersion));

        builder.Services.AddTransient<IMovieRepository, MovieRepository>();
        builder.Services.AddTransient<IActorRepository, ActorRepository>();


        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();
        
        
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}

