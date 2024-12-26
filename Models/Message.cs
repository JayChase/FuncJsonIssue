using System;
using System.Collections.Generic;

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

    public Sentiment[]? Sentiments { get; set; }
}

