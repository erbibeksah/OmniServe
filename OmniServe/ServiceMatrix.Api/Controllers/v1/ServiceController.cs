namespace ServiceMatrix.Api.Controllers.v1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/[controller]/[Action]")]
public class ServiceController : BaseApiController
{
    public ServiceController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
    }

    [HttpGet]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [ActionName("SignIn")]
    public async Task<IActionResult> SignIn()
    {       
        ApiResult apiResult = new ApiResult();
        try
        {
            apiResult.Status = (int)ReturnStatus.Success;
            apiResult.Message = "Successfully Logged In";
            apiResult.Data = "";
            return Ok(apiResult);
        }
        catch (Exception ex)
        {
            return StatusCode((int)ReturnStatus.InternalServerError, ex.Message);
        }
    }
}
