﻿using AutoMapper;
using RecipeApi.Application.Common.Models;
using RecipeApi.Application.Common.Models.SpoonResponse;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Recipe, RecipeViewModel>();
        CreateMap<RecipeInformation, RecipeInformationViewModel>().ReverseMap();
        CreateMap<RecipeCollection, RecipeCollectionViewModel>();

    }
}
