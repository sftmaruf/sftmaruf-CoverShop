namespace CoverShop.Infrastructure.Options;

public class JwtSetting
{
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public string SecretKey { get; set; } = String.Empty;
}
