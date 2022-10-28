using System.Data;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Mdl.WebApi.Configuration;
using Mdl.WebApi.Repositories;
using Mdl.WebApi.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var appConfiguration = builder.Configuration.Get<AppConfiguration>();
builder.Services.AddSingleton(appConfiguration);
builder.Services.AddSingleton(appConfiguration.SmtpConfiguration);
builder.Services.AddSingleton<ISmtpClientFactory, SmtpClientFactory>();
builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(appConfiguration.ConnectionString));
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IMailRepository, MailRepository>();

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