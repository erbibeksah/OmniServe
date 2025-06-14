namespace ServiceMatrix.Api.Utility.Global;

[JsonConverter(typeof(StringEnumConverter))]
public enum ReturnStatus
{
    None,
    Success = 200,
    Created = 201,
    Failed = 400,
    UnAuthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    RequestTimeOut = 408,
    ToManyRequest = 429,
    InternalServerError = 500,
    BadGateway = 502,
    ServiceUnavailable = 503,
    HttpVersionNotSupported = 505,
    NetworkAuthenticationRequired = 511
}
public class ApiResult
{
    public virtual int? Status { get; set; }
    public virtual string? Message { get; set; }
    public virtual object? Data { get; set; }
}
