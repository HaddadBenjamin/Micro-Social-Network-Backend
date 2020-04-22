namespace DiabloII.Domain.Configurations
{
    public class SmtpConfiguration
    {
        public int Port { get; set; }

        public string Host { get; set; }

        public string FromEmail { get; set; }

        public string FromPassword { get; set; }

        public bool EnableService { get; set; }
    }
}
