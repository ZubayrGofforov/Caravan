using Caravan.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Common.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpResponse Response => Accessor.HttpContext.Response;

    public static IHeaderDictionary ResponseHeaders => Response.Headers;

    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static long UserId => GetUserId();

    public static UserRole? UserRole => GetUserRole();
    private static long GetUserId()
    {
        long id;
        bool canParse = long.TryParse(HttpContext.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value, out id);
        return canParse ? id : 0;
    }

    public static UserRole? GetUserRole()
    {
        var res = HttpContext!.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).ToString();
        return res is not null ? Enum.Parse<UserRole>(res) : null;
    }
}