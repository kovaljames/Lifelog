namespace Lifelog.Domain.Api;

public static class Configuration
{
    public static TokenConfiguration Token = new ();
    public static SmtpConfiguration Smtp = new();
    
    public class TokenConfiguration
    {
        public string JwtKey { get; set; } = string.Empty;
        public string FirebaseProject { get; set; } = string.Empty;
    }

    public class SmtpConfiguration
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 25;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}