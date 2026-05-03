using SurveyBasket.Abstractions.Const;
using SurveyBasket.Authentication.Authorization;
using SurveyBasket.Core.Application.Interfaces;

namespace SurveyBasket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("[action]")]
    [HasPermission(PermissionsClaims.GetUsers)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _userService.GetAllAsync(cancellationToken));
    }

    [HttpGet("[action]/{id}")]
    [HasPermission(policyName: PermissionsClaims.GetUsers)]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var result = await _userService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(result.Error.StatusCode);
    }

    [HttpPost("[action]")]
    [HasPermission(PermissionsClaims.AddUsers)]
    public async Task<IActionResult> Add([FromBody] UserCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddAsync(request, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value) : result.ToProblem(result.Error.StatusCode);
    }

    [HttpPatch("[action]/{id}")]
    [HasPermission(PermissionsClaims.AddUsers)]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(id, request, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem(result.Error.StatusCode);
    }

    [HttpGet("[action]/{userId}")]
    [HasPermission(PermissionsClaims.AddUsers)]
    public async Task<IActionResult> DeActiveUser([FromRoute] string userId)
    {
        var result = await _userService.DeActiveUserAccountAsync(userId);
        return result.IsSuccess ? NoContent() : result.ToProblem(400);
    }
}
