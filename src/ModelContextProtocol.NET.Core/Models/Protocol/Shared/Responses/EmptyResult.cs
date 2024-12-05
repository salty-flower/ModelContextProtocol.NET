using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Responses;

public record EmptyResult : Result, IClientMessage, IServerMessage { }
