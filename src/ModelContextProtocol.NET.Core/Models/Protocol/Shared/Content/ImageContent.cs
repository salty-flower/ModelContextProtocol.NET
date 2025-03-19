﻿using System.Text.Json.Serialization;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

public record ImageContent : Annotated
{
    [JsonIgnore]
    public string Type => "image";
    public required string Data { get; init; }
    public required string MimeType { get; init; }
}
