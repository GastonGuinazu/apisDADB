using API.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=localhost;user=root;password=gasti123;database=tpi_dabd"; //Gas
 //var connectionString = "Server=localhost;user=root;password=admin;database=tpi_dabd"; //Noe
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));


// Add services to the container.
builder.Services.AddDbContext<tpi_dabdContext>
(dbContextOptions => dbContextOptions.UseMySql(connectionString,serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddCors();

app.UseCors(c => 
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});


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
