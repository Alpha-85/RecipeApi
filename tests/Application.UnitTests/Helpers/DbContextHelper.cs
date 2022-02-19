using Microsoft.EntityFrameworkCore;
using RecipeApi.Domain.Entities;
using RecipeApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;

namespace Application.UnitTests.Helpers;

public static class DbContextHelper
{
    public static ApplicationDbContext GetApplicationDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

        var weekDays = PopulateWeekdayToDatabase();

        foreach (var day in weekDays)
        {
            applicationDbContext.WeekDays.Add(new WeekDay() { DayOfWeek = day });
        }

        applicationDbContext.SaveChangesAsync();

        return applicationDbContext;
    }

    private static List<string> PopulateWeekdayToDatabase()
    {
        var weekDays = new List<string>
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
        };

        return weekDays;
    }
}
