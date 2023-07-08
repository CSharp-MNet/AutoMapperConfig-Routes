using AutoMapper;
using Microsoft.EntityFrameworkCore;
using si730ebu202023992.API.Inventory.Mapper;
using si730ebu202023992.API.Monitoring.Mapper;
using si730ebu202023992.Domain.Inventory;
using si730ebu202023992.Domain.Inventory.Interface;
using si730ebu202023992.Domain.Monitoring;
using si730ebu202023992.Domain.Monitoring.Interface;
using si730ebu202023992.Infraestructure.Context;
using si730ebu202023992.Infraestructure.Inventory;
using si730ebu202023992.Infraestructure.Inventory.Interface;
using si730ebu202023992.Infraestructure.Monitoring;
using si730ebu202023992.Infraestructure.Monitoring.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependency Injection
builder.Services.AddScoped<IProductInfraestructure, ProductDBInfraestructure>();
builder.Services.AddScoped<IProductDomain, ProductDomain>();
builder.Services.AddScoped<ISnapshotInfraestructure, SnapshotDBInfraestructure>();
builder.Services.AddScoped<ISnapshotDomain, SnapshotDomain>();

//Connection to MySQL
var connectionString = builder.Configuration.GetConnectionString("ConnectionDB");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<si730ebu202023992DBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
    });



//AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ProductToResponse),
    typeof(RequestToProduct),
    typeof(SnapshotToResponse),
    typeof(RequestToSnapshot)
);



var app = builder.Build();


//Create Database
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<si730ebu202023992DBContext>())
{
    context.Database.EnsureCreated();
} 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//Enable CORS
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();