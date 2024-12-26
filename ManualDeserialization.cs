using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using FuncJsonIssue.Models;

namespace FuncJsonIssue;

public class ManualDeserialization
{
    private readonly ILogger<ManualDeserialization> _logger;

    public ManualDeserialization(ILogger<ManualDeserialization> logger)
    {
        _logger = logger;
    }

    [Function("ManualDeserialization")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();

        var message = JsonSerializer.Deserialize<Message>(body, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Converters =
                        {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true)
                        }
        });

        _logger.LogInformation("Message received: {@Message}", message);
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}

