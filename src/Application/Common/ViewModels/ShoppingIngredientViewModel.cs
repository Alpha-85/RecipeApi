using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Application.Common.ViewModels;
[ExcludeFromCodeCoverage]
public class ShoppingIngredientViewModel
{
    public string Name { get; set; }
    public double Amount { get; set; }
    public string Metric { get; set; }
}
