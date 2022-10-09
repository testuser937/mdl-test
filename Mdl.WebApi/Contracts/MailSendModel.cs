namespace Mdl.WebApi.Contracts;

/// <summary>
/// Модель для отправки письма (получаемая от клиента)
/// </summary>
public class MailSendModel
{
    /// <summary>
    /// Создает новый экземпляр модели для отправки письма
    /// </summary>
    /// <param name="subject">Тема</param>
    /// <param name="body">Тело</param>
    /// <param name="recipients">Получатели</param>
    public MailSendModel(string subject, string body, string[] recipients)
    {
        Subject = subject;
        Body = body;
        Recipients = recipients;
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
}