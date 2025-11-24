namespace gentela_Alela.Infrastructure.Email
{
    public class smtpOptions1
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;

        public bool EnableSsl { get; set; } = default!;
        public string FromEmail { get; set; } = default!;
        public string FromName { get; set; } = default!;
    }
}
