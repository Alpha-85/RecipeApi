using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Application.Common.Settings;
[ExcludeFromCodeCoverage]
public class AppSettings
{
    public string Secret { get; set; }

    public int RefreshTokenTTL { get; set; }
}
