namespace ProjectManagement.Api.Shared;

public class JwtSettings
{
    public string Issuer { get; set; } = null!;
    public string Key { get; set; } = null!;
    public TimeSpan Expires { get; set; }
}