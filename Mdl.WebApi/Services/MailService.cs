using System.Collections.Immutable;
using System.Net;
using System.Net.Mail;
using Mdl.WebApi.Configuration;
using Mdl.WebApi.Contracts;
using Mdl.WebApi.Repository;

namespace Mdl.WebApi.Services;

/// <inheritdoc />
public class MailService : IMailService
{
    private readonly IMailRepository _mailRepository;
    private readonly SmtpConfiguration _smtpConfiguration;
    private readonly SmtpClient _smtpClient;

    /// <summary>
    /// Создает новый экземпляр сервиса для работы с письмами
    /// </summary>
    /// <param name="mailRepository">Репозиторий писем</param>
    /// <param name="smtpConfiguration">Конфигурация SMTP</param>
    public MailService(IMailRepository mailRepository, SmtpConfiguration smtpConfiguration)
    {
        _mailRepository = mailRepository;
        _smtpConfiguration = smtpConfiguration;
        _smtpClient = CreateSmtpClient();
    }

    private SmtpClient CreateSmtpClient()
    {
        var smtpClient = new SmtpClient(_smtpConfiguration.SmtpHost, _smtpConfiguration.SmtpPort);
        smtpClient.EnableSsl = _smtpConfiguration.EnableSsl;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(_smtpConfiguration.Login, _smtpConfiguration.Password);
        return smtpClient;
    }

    /// <inheritdoc />
    public Task<ImmutableList<MailReadModel>> GetMails()
    {
        return _mailRepository.GetMails();
    }

    /// <inheritdoc />
    public async Task<MailSendResult> SendMail(MailSendModel mail)
    {
        string? failedMessage = null;
        try
        {
            await SendMailInternal(mail);
        }
        catch (Exception e)
        {
            failedMessage = e.Message;
        }

        var sendResult = string.IsNullOrEmpty(failedMessage)
            ? MailConstants.OkResult
            : MailConstants.FailedResult;

        var mailWriteModel = new MailWriteModel(
            mail.Subject,
            mail.Body,
            mail.Recipients,
            sendResult,
            failedMessage);
        await _mailRepository.SaveMail(mailWriteModel);
        return new MailSendResult(mailWriteModel.Result, mailWriteModel.FailedMessage);
    }

    private async Task SendMailInternal(MailSendModel mail)
    {
        var recipients = string.Join(",", mail.Recipients);
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpConfiguration.FromAddress, _smtpConfiguration.FromDisplayName),
            Subject = mail.Subject,
            Body = mail.Body,
        };
        mailMessage.To.Add(recipients);

        await _smtpClient.SendMailAsync(mailMessage);
    }
}