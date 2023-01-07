using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Web;

namespace Caravan.Api.Configuration
{
    public static class JwtConfiguration
    {
        public static void ConfigureAuth(this WebApplicationBuilder webApplicationBuilder)
        {
            var config = webApplicationBuilder.Configuration.GetSection("Jwt");
        }
    }
}
