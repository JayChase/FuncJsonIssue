# Azure Function App issue: setting JsonSerializerOptions not working: Cosmos input bindings

# Refs

- Documentation on [Customizing JSON serialization](https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide?tabs=hostbuilder%2Cwindows#customizing-json-serialization)
- Related issues: [Unable to override the JsonSerializer when using ConfigureFunctionsWebApplication](https://github.com/Azure/azure-functions-dotnet-worker/issues/2131)

- Note adding the JsonConverter attribute is not an option as the classes are third party (Kiota generated).
