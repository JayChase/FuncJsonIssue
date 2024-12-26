using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

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

host.Run();
