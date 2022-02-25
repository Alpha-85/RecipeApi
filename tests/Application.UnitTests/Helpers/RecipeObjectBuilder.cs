
using System.Collections.Generic;
using RecipeApi.Application.Common.Models.SpoonResponse;

namespace Application.UnitTests.Helpers;

public static class RecipeObjectBuilder
{
    public static List<Recipe> GetListWithSingleRecipe()
    {
        var recipes = new List<Recipe>();

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        return recipes;
    }

    public static List<Recipe> GetListWithThreeRecipes()
    {
        var recipes = new List<Recipe>();

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        return recipes;
    }

    public static List<Recipe> GetListWithFiveRecipes()
    {
        var recipes = new List<Recipe>();

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = true,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                new()
                {
                    Id = 1,
                    Aisle = "",
                    Image = "",
                    Consistency = "",
                    Name = "egg",
                    NameClean = "egg",
                    Original = "egg",
                    OriginalString = "egg",
                    OriginalName = "egg",
                    Amount = 1,
                    Unit = "",
                    Meta = new List<string>(){""},
                    MetaInformation = new List<string>(){""},
                    Measures = new Measures
                    {
                        Us = new Us
                        {
                            Amount = 1,
                            UnitShort = "",
                            UnitLong = ""
                        },
                        Metric = new Metric
                        {
                            Amount = 1,
                            UnitShort = "",
                            UnitLong = ""
                        }
                    }
                }
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                new()
                {
                    Id = 1,
                    Aisle = "",
                    Image = "",
                    Consistency = "",
                    Name = "egg",
                    NameClean = "egg",
                    Original = "egg",
                    OriginalString = "egg",
                    OriginalName = "egg",
                    Amount = 1,
                    Unit = "",
                    Meta = new List<string>(){""},
                    MetaInformation = new List<string>(){""},
                    Measures = new Measures
                    {
                        Us = new Us
                        {
                            Amount = 1,
                            UnitShort = "",
                            UnitLong = ""
                        },
                        Metric = new Metric
                        {
                            Amount = 1,
                            UnitShort = "",
                            UnitLong = ""
                        }
                    }
                }
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });
        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });
        recipes.Add(new Recipe
        {
            Vegetarian = false,
            Vegan = false,
            GlutenFree = false,
            DairyFree = false,
            VeryHealthy = false,
            Cheap = false,
            VeryPopular = false,
            Sustainable = false,
            WeightWatcherSmartPoints = 0,
            Gaps = "",
            LowFodmap = false,
            AggregateLikes = 0,
            SpoonacularScore = 0,
            HealthScore = 0,
            CreditsText = "",
            License = "",
            SourceName = "",
            PricePerServing = 0,
            ExtendedIngredients = new List<ExtendedIngredient>
            {
                Capacity = 1
            },
            Id = 0,
            Title = "",
            ReadyInMinutes = 0,
            Servings = 0,
            SourceUrl = "",
            Image = "",
            ImageType = "",
            Summary = "",
            Cuisines = new List<string>() { "" },
            DishTypes = new List<string>() { "" },
            Diets = new List<string>() { "" },
            Occasions = new List<object>(),
            Instructions = "",
            AnalyzedInstructions = new List<AnalyzedInstruction>
            {
                Capacity = 1
            },
            OriginalId = new object(),
            SpoonacularSourceUrl = ""
        });

        return recipes;
    }
}
