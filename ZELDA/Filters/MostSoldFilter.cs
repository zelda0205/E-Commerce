using Microsoft.AspNetCore.Mvc.Filters;

namespace ZELDA.Filters
{
    public class MostSoldFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sort = context.HttpContext.Request.Query["sort"];
            if (sort == "mostsold")
            {
                context.HttpContext.Items["IsMostSold"] = true;
            }
        }
    }
}

