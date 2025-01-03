# Azure Function App issue: setting JsonSerializerOptions not working with Cosmos input bindings

Issue:

Custom JSON Serialization options are ignored when deserializing CosmosDB input bindings. The main issue for my project is this then leads to Enums in third party classes (Ms Graph SDK) causing an exception.


**Program.cs** (the config)

```csharp
var host = new HostBuilder()
       .ConfigureFunctionsWebApplication((IFunctionsWorkerApplicationBuilder builder) =>
    {
        builder.Services.Configure<JsonSerializerOptions>(jsonSerializerOptions =>
        {
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

```

**Message.cs** (a class with an enum property)

```csharp
public enum Sentiment
{
    Positive,
    Neutral,
    Negative
}

public class Message
{

    public string Id { get; set; } = string.Empty;

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public Sentiment? Sentiment { get; set; }
}
```

**MessageFromBinding.cs** (the http triggered function with the CosmosDb input binding)

```csharp
public enum Sentiment
{
    Positive,
    Neutral,
    Negative
}

public class Message
{

    public string Id { get; set; } = string.Empty;

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public Sentiment? Sentiment { get; set; }
}
```

**The error**

```bash
Error:System.Text.Json.JsonException: The JSON value could not be converted to System.Nullable`1[FuncJsonIssue.Models.Sentiment].
```

## Refs

- Documentation followed for [Customizing JSON serialization](https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide?tabs=hostbuilder%2Cwindows#customizing-json-serialization)
- Related issues: [Unable to override the JsonSerializer when using ConfigureFunctionsWebApplication](https://github.com/Azure/azure-functions-dotnet-worker/issues/2131)

- Note adding the JsonConverter attribute is not an option as the classes are third party (Kiota generated).
