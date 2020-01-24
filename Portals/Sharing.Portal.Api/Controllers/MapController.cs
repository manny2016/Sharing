
namespace Sharing.Portal.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Sharing.Portal.Api.Models;
    using Sharing.Core;
    [Produces("application/json")]
    [ApiController]
    public class MapController : ControllerBase
    {
        [Route("api/map/query")]
        [HttpPost]
        public BaiduAPIQueryResponse Query(MapQueryFilter filter)
        {
            var url = string.Concat(BaiduConstant.SearchAPI, filter.GenernateQueryParameter());
            return url.GetUriJsonContent<BaiduAPIQueryResponse>((http) =>
                {
                    http.Method = "GET";
                    return http;
            });
        }
    }
}