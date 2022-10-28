using System;
using System.IO;
using System.Net.Mail;
using System.Text.Json;
using Mdl.WebApi.Configuration;
using Mdl.WebApi.Services;
using NUnit.Framework;

namespace Mdl.WebApi.Tests;

public class SmtpClientFactoryTests
{
    private readonly ISmtpClientFactory _smtpClientFactory;
    private readonly SmtpConfiguration _smtpConfiguration;

    public SmtpClientFactoryTests()
    {
        _smtpClientFactory = new SmtpClientFactory();
    }

    [Test]
    public void CreateSmtpClientTest()
    {
        var configText = File.ReadAllText("appsettings.json");
        var smtpConfiguration = JsonSerializer.Deserialize<SmtpConfiguration>(configText);
        if (smtpConfiguration == null)
        {
            throw new Exception("Smtp configuration is null");
        }

        var actual = _smtpClientFactory.Create(smtpConfiguration);
        Assert.IsInstanceOf<SmtpClient>(actual);
    }
}