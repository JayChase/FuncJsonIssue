using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuncJsonIssue.Models;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FuncJsonIssue
{
    public class MyHttpTrigger
    {
        private readonly ILogger<MyHttpTrigger> _logger;

        public MyHttpTrigger(ILogger<MyHttpTrigger> logger)
        {
            _logger = logger;
        }

        [Function("MyHttpTrigger")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "myHttpTrigger/{messageId}")] HttpRequest req, [FromBody] Message message,
         [CosmosDBInput(
        databaseName: "%COSMOS_DB_NAME%",
        containerName: "%COSMOS_DB_CONTAINER_NAME%",
        Connection  = "COSMOS_DB_CONNECTION",
        Id = "{messageId}",
        PartitionKey = "{messageId}")] Message cosmosDbMessage)
        {
            _logger.LogInformation("cosmosDbMessage received: {@cosmosDbMessage}", cosmosDbMessage);
            _logger.LogInformation("Message received: {@Message}", message);
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
