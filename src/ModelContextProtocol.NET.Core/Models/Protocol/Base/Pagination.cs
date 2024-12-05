namespace ModelContextProtocol.NET.Core.Models.Protocol.Base;

// From schema.ts:
// export interface PaginatedRequest extends Request {
//   params?: {
//     cursor?: Cursor
//   }
// }
public interface IPaginatedRequestParams
{
    string? Cursor { get; }
}

// From schema.ts:
// export interface PaginatedResult extends Result {
//   nextCursor?: Cursor
// }
public interface IPaginatedResult
{
    string? NextCursor { get; }
}
