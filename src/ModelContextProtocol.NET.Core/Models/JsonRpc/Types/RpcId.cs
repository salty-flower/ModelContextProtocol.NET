using System;
using System.Text.Json.Serialization;
using ModelContextProtocol.NET.Core.Json;

namespace ModelContextProtocol.NET.Core.Models.JsonRpc.Types;

/// <summary>
///   <see cref="RpcId" /> is the implementation class for "string | number".
///   It currently serves as `RequestId` and `ProgressToken`.
///   <para>
///     Schema Reference:
///     <list type="bullet">
///       <item>
///         <see
///           href="https:/// github.com/modelcontextprotocol/specification/blob/main/schema/2024-11-05/schema.ts"
///         >
///           TypeScript version
///         </see>
///         defines both of them to `string | number`
///       </item>
///       <item>
///         <see
///           href="https:/// github.com/modelcontextprotocol/specification/blob/main/schema/2024-11-05/schema.json"
///         >
///           JSON version
///         </see>
///         further restricts to `string | integer`
///         <see
///           href="https:/// github.com/modelcontextprotocol/specification/blob/363e36b3fabad8ec6253e468a74c8f7fc972e539/schema/2024-11-05/schema.json#L1268"
///         >
///           (permalink)
///         </see>
///       </item>
///     </list>
///   </para>
///   Hence it's safe to use <see cref="long" /> for `number` and <see cref="string" /> for `string`.
/// </summary>
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
