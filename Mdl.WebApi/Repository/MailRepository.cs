using System.Collections.Immutable;
using System.Data;
using Dapper;
using Mdl.WebApi.Contracts;

namespace Mdl.WebApi.Repository;

/// <inheritdoc />
public class MailRepository : IMailRepository
{
    private readonly IDbConnection _dbConnection;

    /// <summary>
    /// Создает новый экземпляр репозитория для писем
    /// </summary>
    /// <param name="dbConnection"></param>
    public MailRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    /// <inheritdoc />
    public async Task<ImmutableList<MailReadModel>> GetMails()
    {
        const string query = @"
SELECT
        id,
        creation_date,
        subject,
        body,
        recipients,
        ""result"",
        failed_message
    FROM main.mail;
";
        var mails = await _dbConnection.QueryAsync<MailReadModel>(query);
        return mails.ToImmutableList();
    }

    /// <inheritdoc />
    /// 
    public Task SaveMail(MailWriteModel mail)
    {
        const string query = @"
INSERT INTO main.mail
(
    subject,
    body,
    recipients,
    ""result"",
    failed_message
)
VALUES
(
    :subject,
    :body,
    :recipients,
    :result,
    :failedMessage
);";
        return _dbConnection.ExecuteAsync(query, mail);
    }
}