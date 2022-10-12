using System.Data;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Mdl.WebApi.Configuration;
using Mdl.WebApi.Repository;
using Mdl.WebApi.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appConfiguration = builder.Configuration.Get<AppConfiguration>();
builder.Services.AddSingleton(appConfiguration);
builder.Services.AddSingleton(appConfiguration.SmtpConfiguration);
builder.Services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(appConfiguration.ConnectionString));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IMailRepository, MailRepository>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Mdl API",
        Description = "An ASP.NET Core Web API for managing emails",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();