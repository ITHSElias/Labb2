using Microsoft.EntityFrameworkCore;
using Labb2.Model;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt => 
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<AppDbContext>(o => 
{ 
    string connectionString = builder.Configuration.GetConnectionString("DbCon"); //Vid byte mellan lokal databas och azure byt både denna och DbPassword
    
    //Har inte fått user-secrets att fungera, se över det om det finns tid
    /*var connBuilder = new SqlConnectionStringBuilder(connectionString) 
    {
        Password = builder.Configuration["DbPassword"]
    };*/

    o.UseSqlServer(connectionString); 
    o.LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging(true);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
