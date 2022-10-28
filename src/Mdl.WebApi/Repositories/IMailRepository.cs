using System.Collections.Immutable;
using Mdl.WebApi.Contracts;

namespace Mdl.WebApi.Repositories;

/// <summary>
/// Предоставляет методы БД для писем
/// </summary>
public interface IMailRepository
{
    /// <summary>
    /// Получение списка отправленных писем
    /// </summary>
    /// <returns>Список отправленных писем</returns>
    Task<ImmutableList<MailReadModel>> GetMails();

    /// <summary>
    /// Сохранение письма
    /// </summary>
    /// <param name="mail">Письмо</param>
    /// <returns>Результат операции</returns>
    Task SaveMail(MailWriteModel mail);
}