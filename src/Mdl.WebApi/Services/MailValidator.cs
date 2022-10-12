namespace Mdl.WebApi.Services;

/// <summary>
/// Предоставляет методы валидации электронных писем
/// </summary>
public static class MailValidator
{
    /// <summary>
    /// Валидация email-адреса
    /// </summary>
    /// <param name="mail">E-mail адресс</param>
    /// <returns>Результат валидации (true - валиден, false - невалиден)</returns>
    public static bool IsValidMail(string mail)
    {
        var trimmedEmail = mail.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }

        try
        {
            var address = new System.Net.Mail.MailAddress(mail);
            return address.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}