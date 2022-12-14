using Agency.Api.DTOModels;
using Agency.Api.DTOModels.Journey;
using Agency.Api.DTOModels.Ticket;
using Agency.Api.DTOModels.Vehicle;
using Agency.Core.Contracts;
using Agency.Core.Services.JourneyServices;
using Agency.Core.Services.TicketServices;
using Agency.Core.Services.VehicleServices;
using Agency.Data.DB;
//using Agency.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ITrainService, TrainService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<IBoatService, BoatService>();
builder.Services.AddScoped<IAirplaneService, AirplaneService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<ITicketNode, TicketNode>();
builder.Services.AddScoped<IJourneyNode, JourneyNode>();
builder.Services.AddScoped<IVehicleNode, VehicleNode>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMvcCore().AddDataAnnotations();

//connect the database

//builder.Services.AddDbContext<AgencyDBContext>(opt =>
//opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
//ServiceLifetime.Scoped);
string connectionString = "";
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
if (dbHost == null || dbName == null || dbPassword == null)
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    connectionString = $"Data Source={dbHost};Initial Catalog={dbName}; User ID=sa;Password={dbPassword}";
}
//var connectionString = $"Server={dbHost}; Database = {dbName}; Trusted_Connection = True; Password={dbPassword}";
builder.Services.AddDbContext<AgencyDBContext>(opt =>
    {
        opt.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//added
app.UseRouting();

app.UseCors();
//
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
