using Microsoft.AspNetCore.Http;
using RecipeApi.Application.Common.Interfaces;

namespace RecipeApi.Application.Common.Extensions;

public  class IpAddressExtensions : IIpAddressExtensions
{
    public  string IpAddress(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            return context.Request.Headers["X-Forwarded-For"];
        else
            return context.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
