
using System.Text.Json.Serialization;

namespace RecipeApi.Domain.Entities;

public class User : AuditableEntity
{
    public int Id { get; set; }
    public string UserName { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; set; }
    public List<RecipeCollection> RecipeCollections { get; set; }

}

