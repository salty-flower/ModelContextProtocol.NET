using System;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Json;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

[JsonConverter(typeof(RpcIdJsonConverter))]
public readonly struct RpcId : IEquatable<RpcId>
{
    private readonly object? value;

    public RpcId(string value) => this.value = value;

    public RpcId(long value) => this.value = value;

    public RpcId()
    {
        value = null;
    }

    public bool IsNull => value == null;
    public bool IsString => value is string;
    public bool IsNumber => value is long;

    public string? AsString => value as string;
    public long? AsNumber => value as long?;

    public override string ToString() => value?.ToString() ?? "null";

    public bool Equals(RpcId other) => Equals(value, other.value);

    public override bool Equals(object? obj) => obj is RpcId other && Equals(other);

    public override int GetHashCode() => value?.GetHashCode() ?? 0;

    public static bool operator ==(RpcId left, RpcId right) => left.Equals(right);

    public static bool operator !=(RpcId left, RpcId right) => !left.Equals(right);

    public static implicit operator RpcId(string value) => new(value);

    public static implicit operator RpcId(long value) => new(value);

    public static implicit operator RpcId(int value) => new(value);
}
