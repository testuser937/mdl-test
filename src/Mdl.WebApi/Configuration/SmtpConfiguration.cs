namespace Mdl.WebApi.Configuration;

/// <summary>
/// Настройки SMTP
/// </summary>
public class SmtpConfiguration
{
    /// <summary>
    /// Адрессант
    /// </summary>
    public string FromAddress { get; init; }

    /// <summary>
    /// Отображаемое имя адресанта
    /// </summary>
    public string FromDisplayName { get; init; }

    /// <summary>
    /// Хост сервера SMTP
    /// </summary>
    public string SmtpHost { get; init; }

    /// <summary>
    /// Порт сервера SMTP
    /// </summary>
    public int SmtpPort { get; init; }

    /// <summary>
    /// Логин отправителя
    /// </summary>
    public string Login { get; init; }

    /// <summary>
    /// Пароль отправителя
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Признак использования SSL
    /// </summary>
    public bool EnableSsl { get; init; }
}