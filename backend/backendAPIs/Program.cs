using backendAPIs.Repository;
using backendAPIs.Models;
using backendAPIs.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backendAPIs.Services.Interfaces;
using backendAPIs.Repository.Interfaces;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Authorization",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                 Reference = new OpenApiReference{
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
            }, new string[] {}
        }
    });
});

builder.Services.AddDbContext<LoansContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<ILoanCardRepo, LoanCardRepo>();
builder.Services.AddScoped<IEmployeeRequestDetailRepo, EmployeeRequestDetailRepo>();
builder.Services.AddScoped<IEmployeeLoanCardDetailRepo, EmployeeLoanCardDetailRepo>();
builder.Services.AddScoped<IItemRepo, ItemRepo>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILoanCardService, LoanCardService>();
builder.Services.AddScoped<IEmployeeRequestService, EmployeeRequestService>();
builder.Services.AddScoped<IEmployeeLoanCardDetailService, EmployeeLoanCardDetailService>();
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddDataProtection();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
