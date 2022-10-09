namespace Mdl.WebApi.Configuration;

/// <summary>
/// Конфигурация приложения
/// </summary>
public class AppConfiguration
{
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    public string ConnectionString { get; init; }

    /// <summary>
    /// Настройки SMTP-клиента
    /// </summary>
    public SmtpConfiguration SmtpConfiguration { get; init; }
}