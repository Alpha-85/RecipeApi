using AutoMapper;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Entities;
using System.Text.RegularExpressions;

namespace RecipeApi.Application.Common.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Recipe, RecipeViewModel>()
            .ForMember(dest => dest.Instructions, m => m
            .MapFrom(src => StringSplitter(src.Instructions)));

        CreateMap<RecipeInformation, RecipeInformationViewModel>().ReverseMap();
        CreateMap<RecipeCollection, RecipeCollectionViewModel>();

    }

    private List<string> StringSplitter(string orginalString)
    {
        var result = new List<string>();
        var remainTag = "li";
        var pattern = String.Format("(</?(?!{0})[^<>]*(?<!{0})>)", remainTag);
        var replacedString = Regex.Replace(orginalString, pattern, "");

        string[] seperation = { "</li>", "<li>" ,"\n"};

        string[] sectionedData = replacedString.Split(seperation, StringSplitOptions.RemoveEmptyEntries);
        result.AddRange(sectionedData);
        return result;

    }


}
