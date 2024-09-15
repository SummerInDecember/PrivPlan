using Microsoft.EntityFrameworkCore;
using privplan_api.Models;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);


string? sqlConfig = builder.Configuration.GetConnectionString("MySqlConnection");
// Add services to the container.

builder.Services.AddControllers();
if(sqlConfig != null)
{
    /**
    * Already verified the connection string isnt null
    * this still gives the warning, which i dislike (lol)
    * hence why i suppressed the warning
    */
    #pragma warning disable CS8604 // Possible null reference argument.
    builder.Services.AddDbContext<PrivPlanContext>(opt => opt.UseMySql(sqlConfig,
                                                     ServerVersion.AutoDetect(sqlConfig))); 
    #pragma warning restore CS8604 // Possible null reference argument.
}
else
{
    Console.WriteLine("Where did the connection string go?");
    Environment.Exit(1);
}
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
