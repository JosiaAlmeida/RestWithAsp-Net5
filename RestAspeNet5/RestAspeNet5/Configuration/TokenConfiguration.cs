namespace RestAspeNet5.Configuration
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public long Minutes { get; set; }
        public long DayToExpiry { get; set; }
    }
}
