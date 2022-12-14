using System.Collections.Immutable;
using System.Net.Mail;
using Mdl.WebApi.Configuration;
using Mdl.WebApi.Contracts;
using Mdl.WebApi.Repositories;

namespace Mdl.WebApi.Services;

/// <summary>
/// Сервис для работы с письмами
/// </summary>
public class MailService : IMailService
{
    private readonly IMailRepository _mailRepository;
    private readonly SmtpConfiguration _smtpConfiguration;
    private readonly ISmtpClientFactory _smtpClientFactory;

    /// <summary>
    /// Создает новый экземпляр сервиса для работы с письмами
    /// </summary>
    /// <param name="mailRepository">Репозиторий писем</param>
    /// <param name="smtpConfiguration">Конфигурация SMTP</param>
    /// <param name="smtpClientFactory">Фабрика SMTP-клиентов</param>
    public MailService(
        IMailRepository mailRepository,
        SmtpConfiguration smtpConfiguration,
        ISmtpClientFactory smtpClientFactory)
    {
        _mailRepository = mailRepository;
        _smtpConfiguration = smtpConfiguration;
        _smtpClientFactory = smtpClientFactory;
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
        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpConfiguration.FromAddress, _smtpConfiguration.FromDisplayName),
            Subject = mail.Subject,
            Body = mail.Body,
        };
        mailMessage.To.Add(recipients);

        using var smtpClient = _smtpClientFactory.Create(_smtpConfiguration);
        await smtpClient.SendMailAsync(mailMessage);
    }
}