using System.Net.Mail;
using Mdl.WebApi.Configuration;

namespace Mdl.WebApi.Services;

/// <summary>
/// Предоставляет методы для создания <see cref="SmtpClient"/>
/// </summary>
public interface ISmtpClientFactory
{
    /// <summary>
    /// Создание SMTP-клиента
    /// </summary>
    /// <returns></returns>
    SmtpClient Create(SmtpConfiguration configuration);
}