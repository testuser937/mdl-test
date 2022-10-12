namespace Mdl.WebApi.Contracts;

/// <summary>
/// Результат отправки письма
/// </summary>
public class MailSendResult
{
    public string Result { get; }
    
    public string? FailedMessage { get; }

    /// <summary>
    /// Создает новый экземпляр результата отправки
    /// </summary>
    /// <param name="result">Результат (OK / Failed)</param>
    /// <param name="failedMessage">Ошибка отправки</param>
    public MailSendResult(string result, string? failedMessage = null)
    {
        Result = result;
        FailedMessage = failedMessage;
    }
}