using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectSem3.DTOs;
using ProjectSem3.Hubs;
using ProjectSem3.Models;
using ProjectSem3.Services.AccountService;
using ProjectSem3.Services.AgeGroupService;
using ProjectSem3.Services.BusesSeatService;
using ProjectSem3.Services.BusesTripService;
using ProjectSem3.Services.BusService;
using ProjectSem3.Services.BusTypeService;
using ProjectSem3.Services.LevelService;
using ProjectSem3.Services.LocationService;
using ProjectSem3.Services.PaymentService;
using ProjectSem3.Services.PaypalService;
using ProjectSem3.Services.PolicyService;
using ProjectSem3.Services.TripService;
using System.Text;
using Microsoft.Extensions.Options;
using ProjectSem3.Services.BookingService;

var builder = WebApplication.CreateBuilder(args);
// Read configuration file (appsettings.json)
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddSignalR(); // Thêm dịch vụ SignalR

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(); //cho phép bên ngoài gọi API

//JWT
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    //option.TokenValidationParameters = new TokenValidationParameters

    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});



builder.Services.AddAutoMapper(typeof(DataMapping));
/*// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle*/
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"].ToString();

builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BusesTripService, BusesTripServiceImpl>();
builder.Services.AddScoped<AgeGroupService, AgeGroupServiceImpl>();
builder.Services.AddScoped<LevelService, LevelServiceImpl>();
builder.Services.AddScoped<AccountUserService, AccountUserServiceImpl>();


builder.Services.AddScoped<TripService, TripServiceImpl>();
builder.Services.AddScoped<LocationService, LocationServiceImpl>();
builder.Services.AddScoped<BookingService, BookingServiceImpl>();

builder.Services.AddScoped<BusTypeService, BusTypeServiceImpl>();
builder.Services.AddScoped<BusService, BusServiceImpl>();
builder.Services.AddScoped<BusesSeatService, BusesSeatServiceImpl>();
builder.Services.AddScoped<PaymentService, PaymentServiceImpl>();
builder.Services.AddScoped<PaypalService>();
builder.Services.AddScoped<BookingServiceImpl>();

//builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<PolicyService, PolicyServiceImpl>();



var app = builder.Build();
// Sử dụng Developer Exception Page trong môi trường phát triển
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.
// Cấu hình pipeline cho yêu cầu HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Sử dụng trang ngoại lệ cho nhà phát triển
}
app.UseHttpsRedirection(); // Tự động chuyển hướng sang HTTPS
app.UseRouting(); // Cấu hình định tuyến

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.UseAuthentication(); //kích hoạt midddlewarre xác thực . Ví dụ, nó có thể đảm nhận việc kiểm tra xem người dùng đã đăng nhập chưa trước khi truy cập vào một trang hoặc tài nguyên cụ thể.
// Validate the token

app.UseAuthorization(); // kích hoạt middleware phân quyền. Ví dụ, nó có thể kiểm tra xem người dùng có quyền truy cập vào một trang hay tài nguyên cụ thể không dựa trên vai trò hoặc các yêu cầu quyền.
// Check for user permissions/roles

// Cấu hình endpoint cho SignalR
app.MapHub<SeatHub>("/seatHub");

app.MapControllers();

app.Run();
