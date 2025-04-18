using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters
{
    public class ApiLogginFilters : IActionFilter
    {
        private readonly ILogger<ApiLogginFilters> _logger;

        public ApiLogginFilters(ILogger<ApiLogginFilters> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("#########################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
            _logger.LogInformation("#########################################");

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Executando -> OnActionExecuted");
            _logger.LogInformation("#########################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"Status Code: {context.HttpContext.Response.StatusCode}");
            _logger.LogInformation("#########################################");
        }
    }
}
