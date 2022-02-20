
using System.Diagnostics.CodeAnalysis;

namespace RecipeApi.Domain.Common;
[ExcludeFromCodeCoverage]
public abstract class AuditableEntity
{
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}

