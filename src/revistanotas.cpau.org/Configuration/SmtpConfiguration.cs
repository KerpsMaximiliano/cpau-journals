namespace CPAU.RevistaNotas.Configuration
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string FromName { get; set; }

        public string FromAddress { get; set; }
        
        public string ToName { get; set; }

        public string ToAddress { get; set; }

        public string CcAddress { get; set; }
    }
}
