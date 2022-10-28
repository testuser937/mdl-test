using System;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Mdl.WebApi.Configuration;
using Mdl.WebApi.Contracts;
using Mdl.WebApi.Repositories;
using Mdl.WebApi.Services;
using Npgsql;
using NUnit.Framework;

namespace Mdl.WebApi.Tests;

public class MailServiceTests : IDisposable
{
    private readonly IMailService _mailService;
    private readonly AppConfiguration _appConfiguration;
    private readonly IDbConnection _dbConnection;

    public MailServiceTests()
    {
        _appConfiguration = GetAppConfiguration();
        _dbConnection = new NpgsqlConnection(_appConfiguration.ConnectionString);
        _mailService = new MailService(
            new MailRepository(_dbConnection),
            _appConfiguration.SmtpConfiguration,
            new SmtpClientFactory());
    }

    private AppConfiguration GetAppConfiguration()
    {
        var configText = File.ReadAllText("appsettings.json");
        var configuration = JsonSerializer.Deserialize<AppConfiguration>(configText) ??
                            throw new Exception("appsettings.json is not valid");
        return configuration;
    }

    [Test]
    public async Task GetMailsTest()
    {
        var mails = await _mailService.GetMails();
        Assert.IsNotEmpty(mails);
    }

    [Test]
    public async Task SendMailTest()
    {
        var mail = new MailSendModel("test", "test", new[] { _appConfiguration.SmtpConfiguration.FromAddress });
        await _mailService.SendMail(mail);
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}