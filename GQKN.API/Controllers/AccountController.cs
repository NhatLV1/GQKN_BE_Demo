using AutoMapper.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NuGet.Packaging;
using PVI.GQKN.API.Application.Commands.AccountCommands;
using PVI.GQKN.API.Services.Auth;
using System.Data;

namespace PVI.GQKN.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IMediator mediator;

    private readonly ILogger<AccountController> logger;
 

    public AccountController(
        IMediator mediator,
        ILogger<AccountController> logger
        )
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    /// <summary>
    /// Đăng nhập vào hệ thống GQKN từ tài khoản Pias
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    [Route("login")]
    [HttpPost]
    [ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await mediator.Send(loginRequest);

        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin hồ sơ người dùng
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("profile")]
    [HttpGet]
    [ProducesResponseType(typeof(UserProfile), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetProfile() 
    {
        var profile = await mediator.Send(new GetUserProfileRequest());

        return Ok(profile);
    }

    ///// <summary>
    ///// Tạo tài khoản GQKN từ tài khoản Pias
    ///// </summary>
    ///// <param name="request"></param>
    ///// <returns></returns>
    //[Route("signin")]
    //[HttpPost]
    //[ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.BadRequest)]
    //public async Task<IActionResult> SignIn([FromBody] PiasSignInRequest request)
    //{
    //    var result = await mediator.Send(request);

    //    return Ok(result);
    //}


    /// <summary>
    /// Yêu cầu cấp token từ hệ thống GQKN
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    //[Route("refreshtoken")]
    //[HttpPost]
    //[ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.OK)]
    //[ProducesResponseType(typeof(LoginResult), (int)HttpStatusCode.BadRequest)]
    //public async Task<IActionResult> RequestToken([FromBody] RefreshTokenRequest request)
    //{
    //    var result = await mediator.Send(request);
    //    return Ok(result);
    //}

    //[Route("resetpassword")]
    //[HttpPost]
    //[ProducesResponseType((int)HttpStatusCode.OK)]
    //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
    //public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    //{
    //    var result = await mediator.Send(request);

    //    if (result)
    //        return Ok();

    //    return BadRequest();
    //}

}

