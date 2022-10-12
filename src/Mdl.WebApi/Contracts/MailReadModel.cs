namespace Mdl.WebApi.Contracts;

/// <summary>
/// Модель для чтения письма
/// </summary>
public class MailReadModel
{
    /// <summary>
    /// Создает новый экземпляр модели для чтения письма
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="subject">Тема</param>
    /// <param name="body">Тело</param>
    /// <param name="recipients">Получатели</param>
    /// <param name="result">Результат отправки</param>
    /// <param name="failedMessage">Ошибка отправки</param>
    public MailReadModel(
        long id,
        string subject,
        string body,
        string[] recipients,
        string result,
        string? failedMessage)
    {
        Id = id;
        Subject = subject;
        Body = body;
        Recipients = recipients;
        Result = result;
        FailedMessage = failedMessage;
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Тема
    /// </summary>
    public string Subject { get; }

    /// <summary>
    /// Тело
    /// </summary>
    public string Body { get; }

    /// <summary>
    /// Получатели
    /// </summary>
    public string[] Recipients { get; }

    /// <summary>
    /// Результат отправки (OK / Failed)
    /// </summary>
    public string Result { get; }

    /// <summary>
    /// Ошибка отправки письма
    /// </summary>
    public string? FailedMessage { get; }

    /// <summary>
    /// Конструктор для Dapper
    /// </summary>
    private MailReadModel()
    {
    }
}