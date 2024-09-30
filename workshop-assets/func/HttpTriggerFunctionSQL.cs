using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace func
{
    public class Payload
    {
        public string? currency { get; set; }
    }

    public class HttpTriggerFunctionSQL(ILogger<HttpTriggerFunctionSQL> logger)
    {
        private readonly ILogger<HttpTriggerFunctionSQL> _logger = logger;

        [Function(nameof(ConvertCurrency))]
        public async Task<IActionResult> ConvertCurrency([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string? currency = req.Query["currency"];
    
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (!string.IsNullOrEmpty(requestBody))
            {
                Payload? data = JsonSerializer.Deserialize<Payload>(requestBody);
                Console.WriteLine(JsonSerializer.Serialize(data));
                currency ??= data?.currency;
            }
            currency ??= "USD";

            double conversion = currency switch
            {
                "JPY" => 147.81,
                "EUR" => 0.93,
                _ => 1
            };

            return new OkObjectResult(new {currency = $"{currency}", priceConversion = $"{conversion}"});
        }

        [Function(nameof(GetKeys))]
        public IActionResult GetKeys([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string openAIKey = Environment.GetEnvironmentVariable("OPENAI_KEY") ?? "N/A";
            string languageServiceKey = Environment.GetEnvironmentVariable("LANGUAGE_SERVICE_KEY") ?? "N/A";
            string contentSafeteKey = Environment.GetEnvironmentVariable("CONTENT_SAFETY_KEY") ?? "N/A";
            string message = $"The key for OpenAI is {openAIKey}. The key for AI Language is {languageServiceKey}. The key for Content Safety is {contentSafeteKey}.";
            
            return new OkObjectResult(message);
        }
    }
}
