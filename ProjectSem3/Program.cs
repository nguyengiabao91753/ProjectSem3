using Microsoft.EntityFrameworkCore;
using ProjectSem3.DTOs;
using ProjectSem3.Models;
using ProjectSem3.Services.AgeGroupService;
using ProjectSem3.Services.LocationService;
using ProjectSem3.Services.TripService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers();
builder.Services.AddCors(); //cho phép bên ngoài gọi API



builder.Services.AddAutoMapper(typeof(DataMapping));
/*// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle*/
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"].ToString();

builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<AgeGroupService, AgeGroupServiceImpl>();
builder.Services.AddScoped<TripService, TripServiceImpl>();
builder.Services.AddScoped<LocationService, LocationServiceImpl>();



var app = builder.Build();
app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
