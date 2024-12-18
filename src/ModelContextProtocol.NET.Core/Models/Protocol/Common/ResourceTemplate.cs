﻿using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record ResourceTemplate : Annotated
{
    public required string UriTemplate { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? MimeType { get; init; }
}