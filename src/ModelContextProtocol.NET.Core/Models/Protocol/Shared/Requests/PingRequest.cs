using ModelContextProtocol.NET.Core.Models.Protocol.Base;

namespace ModelContextProtocol.NET.Core.Models.Protocol.Shared.Requests;

public class PingRequest
    : Request<RequestParams<PingRequest.Meta>, PingRequest.Meta>,
        IClientMessage,
        IServerMessage
{
    public override string Method => "ping";

    public class Meta : RequestParams<Meta>.Meta { }
}
