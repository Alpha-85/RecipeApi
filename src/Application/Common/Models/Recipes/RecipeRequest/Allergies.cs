using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Application.Common.Models.Recipes;
[ExcludeFromCodeCoverage]
public class Allergies
{
    public bool IsGlutenFree { get; set; }
    public bool IsDairyFree { get; set; }
    public string OtherAllergies { get; set; }
}
