﻿using System.Collections.Generic;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public record ToolInputSchema
{
    public string? Type { get; init; }
    public IDictionary<string, object>? Properties { get; init; }
    public IReadOnlyList<string>? Required { get; init; }
}
