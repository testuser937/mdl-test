namespace Mdl.WebApi.Contracts;

/// <summary>
/// Модель для записи письма в БД
/// </summary>
public class MailWriteModel
{
    /// <summary>
    /// Создает новый экземпляр модели для записи письма
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <param name="recipients"></param>
    /// <param name="result"></param>
    /// <param name="failedMessage"></param>
    public MailWriteModel(
        string subject,
        string body,
        string[] recipients,
        string result,
        string? failedMessage = null)
    {
        Subject = subject;
        Body = body;
        Recipients = recipients;
        Result = result;
        FailedMessage = failedMessage;
    }

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
    /// Ошибка отсылки письма
    /// </summary>
    public string? FailedMessage { get; }
}