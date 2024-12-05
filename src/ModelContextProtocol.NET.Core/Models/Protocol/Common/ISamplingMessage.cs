using ModelContextProtocol.NET.Core.Models.Protocol.Shared.Content;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Common;

public interface ISamplingMessage
{
    Role Role { get; }
    Annotated Content { get; }
}
