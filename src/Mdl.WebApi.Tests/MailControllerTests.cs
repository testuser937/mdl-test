using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Mdl.WebApi.Contracts;
using Mdl.WebApi.Controllers;
using Mdl.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Mdl.WebApi.Tests;

public class MailControllerTests
{
    private readonly IMailService _mailService;
    private readonly MailSendModel _mailToSend;

    public MailControllerTests()
    {
        _mailToSend = new MailSendModel("", "", new[] { "test@test.com" });
        var mailService = new Mock<IMailService>();
        mailService
            .Setup(x => x.GetMails())
            .Returns(Task.FromResult(new[]
                {
                    new MailReadModel(1, "", "", Array.Empty<string>(), "", null)
                }
                .ToImmutableList()));
        mailService
            .Setup(x => x.SendMail(_mailToSend))
            .Returns(Task.FromResult(new MailSendResult("OK")));
        _mailService = mailService.Object;
    }

    [Test]
    public async Task GetMailsTest()
    {
        var controller = new MailController(_mailService);
        var mails = await controller.GetMails();
        if (mails.Value == null)
        {
            throw new Exception("Mail list is null");
        }

        Assert.IsNotEmpty(mails.Value);
    }

    [Test]
    public async Task SendMailTest()
    {
        var controller = new MailController(_mailService);
        IActionResult sendResult = await controller.SendMails(_mailToSend);
        Assert.IsInstanceOf<OkResult>(sendResult);
    }
}