using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectSem3.DTOs;
using ProjectSem3.Hubs;
using ProjectSem3.Jobs;
using ProjectSem3.Models;
using ProjectSem3.Services.AccountService;
using ProjectSem3.Services.AgeGroupService;
using ProjectSem3.Services.BookingService;
using ProjectSem3.Services.BusesSeatService;
using ProjectSem3.Services.BusesTripService;
using ProjectSem3.Services.BusSeatandTripUpdate;
using ProjectSem3.Services.BusService;
using ProjectSem3.Services.BusTypeService;
using ProjectSem3.Services.LevelService;
using ProjectSem3.Services.LocationService;
using ProjectSem3.Services.PaymentService;
using ProjectSem3.Services.PaypalService;
using ProjectSem3.Services.PolicyService;
using ProjectSem3.Services.TripService;
using ProjectSem3.Services.VNPay;
using Quartz;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Read configuration file (appsettings.json)
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);




//AUTHORIZE HEADER
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectSemester3", Version = "v1" });

    // Thêm cấu hình để Swagger hỗ trợ JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\n\nExample: \"Bearer 12345abcdef\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSignalR(); // Thêm dịch vụ SignalR

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors();




//JWT
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
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
builder.Services.AddScoped<VnPayService, VnPayServiceImpl>();
//builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<PolicyService, PolicyServiceImpl>();


builder.Services.AddHostedService<BusSeatandTripUpdate>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("TripSeatStatusJob");
    q.AddJob<TripSeatStatusJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
       .ForJob(jobKey)
       .WithIdentity("TripSeatStatusTrigger")
       .StartNow() // Chạy ngay lập tức khi ứng dụng khởi động
   );
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("TripSeatStatusTrigger")
        .WithCronSchedule("0 0 * * * ?")); // Chạy mỗi giờ
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


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
