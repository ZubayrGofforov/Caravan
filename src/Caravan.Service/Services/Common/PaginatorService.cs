using Caravan.Service.Common.Utils;
using Caravan.Service.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Caravan.Service.Services.Common
{
    public class PaginatorService : IPaginatorService
    {
        private readonly HttpContextAccessor _accessor;
        public PaginatorService(HttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public void ToPaginator(PaginationMetaData metaData)
        {
            _accessor.HttpContext.Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(metaData));   
        }
    }
}
