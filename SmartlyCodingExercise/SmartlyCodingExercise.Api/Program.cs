using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartlyCodingExercise.Api.Configurations;
using SmartlyCodingExercise.Api.Data;
using SmartlyCodingExercise.Api.Repository;
using SmartlyCodingExercise.Api.Repository.Interfaces;

string AllowSpecificOrigins = "_allowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SCEDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeSalaryRepository, EmployeeSalaryRepository>();
builder.Services.AddScoped<ISalaryHelperRepository, SalaryHelperRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(
                "https://localhost:7111"
            );
            builder.AllowAnyHeader();
            builder.AllowCredentials();
            builder.AllowAnyMethod();
            builder.WithExposedHeaders("Content-Disposition");
        });
});

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
