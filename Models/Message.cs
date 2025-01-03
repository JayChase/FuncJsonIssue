
using System.Text.Json.Serialization;

namespace FuncJsonIssue.Models;

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

