namespace ServiceMatrix.Api.Infrastructure;
public class BaseApiController : ControllerBase
{
    public IHttpContextAccessor _httpContextAccessor;
    public HttpContext _httpContext;
    public BaseApiController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        if (_httpContextAccessor is not null)
        {
            _httpContext = _httpContextAccessor.HttpContext;
        }
    }
}
