using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FuncJsonIssue.Models;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace FuncJsonIssue;

public class MessageFromBody
{
    private readonly ILogger<MessageFromBody> _logger;

    public MessageFromBody(ILogger<MessageFromBody> logger)
    {
        this._logger = logger;
    }

    [Function("MessageFromBody")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "messageFromBody/{messageId}")] HttpRequest req, [FromBody] Message message)
    {
        //_logger.LogInformation("cosmosDbMessage received: {@cosmosDbMessage}", cosmosDbMessage);
        _logger.LogInformation("Message received: {@Message}", message);
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}

