using System.Collections.Immutable;
using Mdl.WebApi.Contracts;

namespace Mdl.WebApi.Services;

/// <summary>
/// Предоставляет методы для получения/отправки писем
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Отправление письма
    /// </summary>
    /// <param name="mail">Письмо</param>
    /// <returns>Результат операции</returns>
    Task<MailSendResult> SendMail(MailSendModel mail);

    /// <summary>
    /// Получение списка отправленных писем
    /// </summary>
    /// <returns>Список отправленных писем</returns>
    Task<ImmutableList<MailReadModel>> GetMails();
}