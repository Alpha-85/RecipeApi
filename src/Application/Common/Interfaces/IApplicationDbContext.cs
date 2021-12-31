﻿using Microsoft.EntityFrameworkCore;
using RecipeApi.Domain.Entities;

namespace RecipeApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
