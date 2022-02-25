
using RecipeApi.Application.Common.Settings;

namespace Application.UnitTests.Helpers;

public static class SettingsHelper
{
    public static AppSettings GetAppSettings()
    {
        var appSetting = new AppSettings()
        {
            Secret = "d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423d3fl423",
            RefreshTokenTTL = 7
        };

        return appSetting;
    }
}
