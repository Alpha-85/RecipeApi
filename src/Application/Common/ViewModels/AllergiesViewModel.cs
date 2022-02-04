using System.Text.Json.Serialization;

namespace RecipeApi.Application.Common.Models;
[Serializable]
public class AllergiesViewModel
{
    [JsonPropertyName("milk")]
    public bool IsMilk { get; set; }
    [JsonPropertyName("gluten")]
    public bool IsGluten { get; set; }
    [JsonPropertyName("nuts")]
    public bool IsNuts { get; set; }
    [JsonPropertyName("egg")]
    public bool IsEgg { get; set; }
    [JsonPropertyName("shellfish")]
    public bool IsShellfish { get; set; }
}
