using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Authorization", Version = "v1"
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
    options.AddSecurityRequirement( new OpenApiSecurityRequirement
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
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EmployeeDataProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddCors();
builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseRouting();

app.MapControllers();

app.Run();
