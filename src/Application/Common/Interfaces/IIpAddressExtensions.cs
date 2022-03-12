using Microsoft.AspNetCore.Http;

namespace RecipeApi.Application.Common.Interfaces;

public interface IIpAddressExtensions
{
    string IpAddress(HttpContext context);
}
