using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace func
{
    public class HttpTriggerFunctionSQL(ILogger<HttpTriggerFunctionSQL> logger)
    {
        private readonly ILogger<HttpTriggerFunctionSQL> _logger = logger;

        [Function(nameof(ConvertCurrency))]
        public IActionResult ConvertCurrency([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, [FromBody] dynamic data)
        {
            string? currency = req.Query["currency"];
            currency ??= data?.currency ?? "USD";
            
            double conversion = currency switch
            {
                "JPY" => 147.81,
                "EUR" => 0.93,
                _ => 1
            };

            return new OkObjectResult(new {currency = $"{currency}", priceConversion = $"{conversion}"});
        }

        [Function(nameof(GetKeys))]
        public IActionResult GetKeys([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, [FromBody] dynamic data)
        {
            string openAIKey = Environment.GetEnvironmentVariable("OPENAI_KEY") ?? "N/A";
            string languageServiceKey = Environment.GetEnvironmentVariable("LANGUAGE_SERVICE_KEY") ?? "N/A";
            string contentSafeteKey = Environment.GetEnvironmentVariable("CONTENT_SAFETY_KEY") ?? "N/A";
            string message = $"The key for OpenAI is {openAIKey}. The key for AI Language is {languageServiceKey}. The key for Content Safety is {contentSafeteKey}.";
            
            return new OkObjectResult(message);
        }
    }
}
