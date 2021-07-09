using Identity.API.Attributes;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Exceptions;
using Identity.Application.Features.Permission.V1.Commands.CheckCustomerPermission;
using Identity.Application.Features.Token.V1;
using Identity.Application.Features.Token.V1.Commands.RefreshToken;
using Identity.Application.Features.Token.V1.Commands.SignInUser;
using Identity.Application.Models.UserContext;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.API.Controllers.V1
{
    [Route("Api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ConnectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        public ConnectController(IMediator mediator, IUserContextService userContextService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        /// <summary>
        /// SignIn, Get Token By User Credentials 
        /// </summary>
        [HttpPost("Token")]
        [ProducesResponseType(typeof(TokenV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Token([FromBody] SignInUserV1Command request)
        {
            TokenV1Response result = await _mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Refresh Token By Expired Access Token And Valid Refresh Token
        /// </summary>
        [HttpPut("Token")]
        [ProducesResponseType(typeof(TokenV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenV1Command request)
        {
            TokenV1Response result = await _mediator.Send(request);
            return Ok(result);
        }

        ///// <summary>
        ///// SignIn, Get Token By User Google Credentials (oAuth) 
        ///// </summary>
        //[HttpPost("Google")]
        //[ProducesResponseType(typeof(SignInWithSocialResponseModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ValidationMessageModel>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ErrorMessageModel), StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> CreateGoogleAsync([FromBody] UserSignInWithGoogleRequestModel model)
        //{
        //    SignInResponseModel result = await _identityManager.SignInWithGoogleAsync(model);

        //    return Ok(result);
        //}

        ///// <summary>
        ///// SignIn, Get Token By User Facebook Credentials (oAuth) 
        ///// </summary>
        //[HttpPost("Facebook")]
        //[ProducesResponseType(typeof(SignInWithSocialResponseModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ValidationMessageModel>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ErrorMessageModel), StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> CreateFacebookAsync([FromBody] UserSignInWithFacebookRequestModel model)
        //{
        //    SignInResponseModel result = await _identityManager.SignInWithFacebookAsync(model);
        //    return Ok(result);
        //}

        


        [Authorization]
        [HttpPost("Authorize")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Authorize([FromBody] CheckCustomerPermissionV1Command request)
        {
            UserContext userContextModel = _userContextService.GetUserContext();

            request.RoleId = _userContextService.GetUserContext().RoleId;

            await _mediator.Send(request);

            return Ok(userContextModel);
        }
    }
}
