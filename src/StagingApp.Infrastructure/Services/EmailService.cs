namespace StagingApp.Infrastructure.Services;
public sealed class EmailService : IEmailService
{
    private static readonly Logger _logger= LogManager.GetCurrentClassLogger();
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateEmailMessage<T>(T model)
    {
        StringBuilder sb = new();
        var descriptions = ModelDescriptionPropertyList.GetDescriptions(model!);

        sb.Append(@"<!DOCTYPE html>
                            <html lang=""en"">

                            <head>
                                <meta charset=""UTF-8"">
                                <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                <title> Staging Complete </title>
                            </head>

                            <body style=""font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #d9d9d9; font-size: 48px; font-weight: 800;"">
    							<div class=""header"" style=""margin: auto; width: 75%; text-align: center;"">
									<div class= ""header-text""> Terminal Staging is Complete!</div>
								</div>
								<hr>
								<table style=""margin: auto;"">
			 						<tbody> ");
        foreach (var property in descriptions)
        {
            sb.AppendLine(@$"<tr style=""font-size: 24px; font-weight: 400; text-align: center;""><td><strong>{property.Description}:</strong></td><td>{property.Source}</td></tr>");
        }
        sb.AppendLine(@$"<tr style=""font-size: 24px; font-weight: 400; text-align: center;""><td><strong>Date\ Time Completed:</strong></td><td>{DateTime.Now}</td></tr>");
        sb.Append(@"</tbody>
					            </table>
                            </body>
                         </html>");

        string output = sb.ToString();
        return output;
    }

    public void SendEmailMessage(string fromString, string subject, string body)
    {
        string? toString = _config.GetValue<string>("EmailSettings:ToAddress");
        string? displayName = _config.GetValue<string>("EmailSettings:ToDisplayName");
        string? host = _config.GetValue<string>("EmailSettings:SmtpHost");

        MailAddress toAddress = new(toString!, displayName);
        _logger.Info("Setting \"To Address\" to: {address}", toAddress.Address);

        MailAddress fromAddress = new(fromString);
        _logger.Info("Setting \"From Address\" to: {address}", fromAddress.Address);

        MailMessage mail = new()
        {
            From = fromAddress,
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mail.To.Add(toAddress);

        SmtpClient client = new(host);

        try
        {
            client.Send(mail);
        }
        catch (SmtpException ex)
        {
            _logger.Error("Error sending mail.");
            _logger.Error(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error("Error sending mail.");
            _logger.Error(ex.Message);
            throw;
        }
    }
}
