namespace gentela_Alela.Infrastructure.Email
{
    public interface IEmailSender1
    {

        Task SendAsync(string toEmail, string subject, string htmlBody, string? plainTextBody = null);
    }
}
