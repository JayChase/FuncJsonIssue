using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuncJsonIssue.Models;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FuncJsonIssue;
public class MessageFromBinding
{
    private readonly ILogger<MessageFromBinding> _logger;

    public MessageFromBinding(ILogger<MessageFromBinding> logger)
    {
        _logger = logger;
    }

    [Function("MessageFromBinding")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "messageFromBinding/{messageId}")] HttpRequest req,
     [CosmosDBInput(
    databaseName: "%COSMOS_DB_NAME%",
    containerName: "%COSMOS_DB_CONTAINER_NAME%",
    Connection  = "COSMOS_DB_CONNECTION",
    Id = "{messageId}",
    PartitionKey = "{messageId}")] Message cosmosDbMessage)
    {
        _logger.LogInformation("cosmosDbMessage received: {@cosmosDbMessage}", cosmosDbMessage);
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}

