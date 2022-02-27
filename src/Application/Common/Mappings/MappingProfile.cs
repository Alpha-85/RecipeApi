using AutoMapper;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.Recipes;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Entities;
using System.Text.RegularExpressions;

namespace RecipeApi.Application.Common.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Recipe, RecipeViewModel>()
            .ForMember(dest => dest.Instructions,
                m => m
                    .MapFrom(src => StringSplitter(src.Instructions)));

        CreateMap<RecipeInformation, RecipeInformationViewModel>()
            .ReverseMap();
        CreateMap<RecipeCollection, RecipeCollectionResponse>();
        CreateMap<AllergiesViewModel, Allergies>()
            .ForMember(dest => dest.IsDairyFree,
                m => m
                    .MapFrom(src => src.IsMilk))
            .ForMember(dest => dest.IsGlutenFree,
                m => m
                    .MapFrom(src => src.IsGluten))
            .ForMember(dest => dest.OtherAllergies,
                m => m.MapFrom(src =>
                    AllergiesConcat(src.IsEgg,
                        src.IsNuts,
                        src.IsShellfish)));


        CreateMap<RecipeDay, RecipeDayResponse>()
            .ForMember(dest => dest.Weekday,
                m => m.MapFrom(src => src.Weekday.DayOfWeek))
            .ForMember(dest => dest.RecipesList,
                opt => opt.MapFrom(
                    src => src.Recipes.Select(r => r)));

    }

    private static List<string> StringSplitter(string originalString)
    {
        var result = new List<string>();
        var remainTag = "li";
        var pattern = string.Format("(</?(?!{0})[^<>]*(?<!{0})>)", remainTag);
        var replacedString = Regex.Replace(originalString, pattern, "");

        string[] separation = { "</li>", "<li>", "\n" };

        var sectionedData = replacedString.Split(separation, StringSplitOptions.RemoveEmptyEntries);
        result.AddRange(sectionedData);
        return result;

    }

    private static string AllergiesConcat(bool isEgg, bool isNuts, bool isShellfish)
    {
        var result = new List<string>();


        if (isEgg) result.Add("egg");
        if (isNuts) result.Add("nut");
        if (isShellfish) result.AddRange(new[] { "prawn", "shrimp", "crab", "lobster", "squid", "oyster", "scallop" });
        if (result.Count == 0) result.Add("");

        return result.Count > 1 ? string.Join(",", result) : result.First();
    }
}
