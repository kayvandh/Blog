namespace Blog.Api.ApplicationSettings
{
    public class Main
    {
        public Main()
        {

        }
        public const string Section = "AppSettings";
        public JwtSetting JwtSettings { get; set; }
    }

    public class JwtSetting
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
