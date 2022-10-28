using System.Net;
using System.Net.Mail;
using Mdl.WebApi.Configuration;

namespace Mdl.WebApi.Services;

/// <inheritdoc />
public class SmtpClientFactory : ISmtpClientFactory
{
    public SmtpClient Create(SmtpConfiguration configuration)
    {
        var smtpClient = new SmtpClient(configuration.SmtpHost, configuration.SmtpPort);
        smtpClient.EnableSsl = configuration.EnableSsl;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(configuration.Login, configuration.Password);
        return smtpClient;
    }
}