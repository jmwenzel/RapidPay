namespace RapidPay.Infrastructure.Core.Settings
{
    public class OAuthSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SymmetricSecurityKey { get; set; }
    }
}
