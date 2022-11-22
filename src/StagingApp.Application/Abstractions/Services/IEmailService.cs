namespace StagingApp.Application.Abstractions.Services;
public interface IEmailService
{
    string CreateEmailMessage<T>(T model);
    void SendEmailMessage(string fromString, string subject, string body);
}
