using RecipeApi.Domain.Entities;
using RecipeApi.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.UnitTests.Helpers;

public static class UserObjectBuilder
{
    public static User GetDefaultUser()
    {
        var user = new User()
        {
            Id = 1,
            UserName = "Alfons",
            PasswordHash = "",
            RefreshTokens = new List<RefreshToken>()
            {
                new()
                {
                    Id = 1,
                    Token = "ABC_DET_231",
                    Created = DateTime.MinValue,
                    Expires = DateTime.MaxValue,
                    CreatedByIp = "192.168.0.1",
                    Revoked = null,
                    ReplacedByToken = "",
                    ReasonRevoked = "",
                    RevokedByIp = "",

                }
            },
            RecipeCollections = new List<RecipeCollection>()
            {
                new()
                {
                    Id = 1,
                    UserId = 1,
                    CollectionName = "AlfonsFavvisar",
                    RecipeDays = new List<RecipeDay>()
                    {
                        new()
                        {
                            Id = 1,
                            RecipeCollectionId = 1,
                            Recipes = new List<RecipeInformation>()
                            {
                                new()
                                {
                                    Created = DateTime.MinValue,
                                    LastModified = null,
                                    Id = 1,
                                    RecipeDayId = 1,
                                    RecipeId = 44321,
                                    RecipeName = "",
                                    RecipeUrl = "",
                                    ReadyInMinutes = 30,
                                    Image = "",
                                    MealType = MealType.Breakfast
                                }
                            }
                        }
                    }

                }
            },

        };
        return user;
    }
}
