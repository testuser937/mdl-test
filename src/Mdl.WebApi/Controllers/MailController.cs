using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Mdl.WebApi.Contracts;
using Mdl.WebApi.Services;

namespace Mdl.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с электронными письмами
/// </summary>
[ApiController]
[Route("api/mails")]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;

    /// <summary>
    /// Создает новый экземпляр контроолера писем
    /// </summary>
    /// <param name="mailService">Сервис для работы с письмами</param>
    public MailController(IMailService mailService)
    {
        _mailService = mailService;
    }

    /// <summary>
    /// Отправление письма
    /// </summary>
    /// <param name="mail">Письмо</param>
    /// <returns>Результат отправки</returns>
    [HttpPost]
    public async Task<ActionResult> SendMails(MailSendModel mail)
    {
        if (!mail.Recipients.Any())
        {
            return BadRequest("Пустой список получателей");
        }

        if (mail.Recipients.Any(r => !MailValidator.IsValidMail(r)))
        {
            return BadRequest("В списке получателей некорректный email");
        }

        var sendResult = await _mailService.SendMail(mail);
        if (!string.IsNullOrEmpty(sendResult.FailedMessage))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, sendResult.FailedMessage);
        }

        return Ok();
    }

    /// <summary>
    /// Получение списка отправленных писем
    /// </summary>
    /// <returns>Список отправленных писем</returns>
    [HttpGet]
    public async Task<ActionResult<ImmutableList<MailReadModel>>> GetMails()
    {
        return await _mailService.GetMails();
    }
}