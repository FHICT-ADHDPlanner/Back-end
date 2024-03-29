using Microsoft.EntityFrameworkCore;
using DataLayer.Context;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Json;
using MvcJsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;
using DataLayer;
using InterfaceLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// JSON Serializer Configuration
builder.Services.Configure<JsonOptions>(o =>
{
    o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<MvcJsonOptions>(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TaskContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PlannerDatabase"))
);

builder.Services.AddScoped<ITaskCollection, TaskDatabase>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Task API",
        Description = "An API used for making tasks"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    opt.DescribeAllParametersInCamelCase();
});

var app = builder.Build();

try
{
    using (IServiceScope serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        DbContext context = serviceScope.ServiceProvider.GetRequiredService<TaskContext>();
        context.Database.EnsureCreated();
        context.Database.Migrate();
    }
} catch (Exception e)
{
    Console.WriteLine("Migration failed: " + e.ToString());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
