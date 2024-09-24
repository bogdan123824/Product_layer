using Microsoft.EntityFrameworkCore;
using Shop.BusinessLayer.Interfaces;
using Shop.BusinessLayer.Services;
using Shop.DataAccessLayer.EF;
using Shop.DataAccessLayer.Interfaces;
using Shop.DataAccessLayer.Repositories;
using Shop.Mapping;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<DbProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnect")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCors",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEF>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");

app.MapControllers();

app.Run();
