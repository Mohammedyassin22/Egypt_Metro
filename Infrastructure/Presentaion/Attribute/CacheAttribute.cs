using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System.Text;


namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInsec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var Cacheservice = context.HttpContext.RequestServices.GetRequiredService<ISerivcesManager>().CacheServices;
            var cachekey = GenerateCache(context.HttpContext.Request);
            var result = await Cacheservice.GetCacheValueAsync(cachekey);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult
                {
                    ContentType = "application/json",
                    Content = result,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return;
        }
     private string GenerateCache(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
