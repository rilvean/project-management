using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Api.Shared;

public sealed class JwtSettings
{
    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; } = null!;

    public TimeSpan Expires { get; set; }
}