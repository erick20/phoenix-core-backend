using Identity.API.Attributes;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Exceptions;
using Identity.Application.Features.Credential.V1.Commands.UpdateCredentialPassword;
using Identity.Application.Features.Credential.V1.Commands.UpdateCredetialState;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Controllers.V1
{
    [Route("Api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CredentialController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        public CredentialController(IMediator mediator, IUserContextService userContextService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        /// <summary>
        /// Create Credential From Customer Micro
        /// </summary>
        [HttpPost]
        [Authorization]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCredential([FromBody] UpdateCredentialPasswordV1Command request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        /// <summary>
        /// Update Credential Password From Customer Micro
        /// </summary>
        [HttpPut("Password/{credentialId}")]
        [Authorization]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCredentialPassword(int credentialId, [FromBody] UpdateCredentialPasswordV1Command request)
        {
            request.CredentialId = credentialId;
            await _mediator.Send(request);

            return Ok();
        }

        /// <summary>
        /// Update Credential State From Customer Micro
        /// </summary>
        [HttpPut("State/{credentialId}")]
        [Authorization]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCredentialState(int credentialId, [FromBody] UpdateCredentialStateV1Command request)
        {
            request.CredentialId = credentialId;
            await _mediator.Send(request);

            return Ok();
        }



        ///// <summary>
        ///// Reset Customer Password Via Email
        ///// </summary>
        //[HttpPost("ResetPassword")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequestModel model)
        //{
        //    await _identityManager.ResetPasswordAsync(model);
        //    return Ok();
        //}

        ///// <summary>
        ///// Confirmation Of Reseted Password
        ///// </summary>
        //[HttpPost("ConfirmPassword")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> ConfirmPasswordAsync([FromBody] ConfirmResetPasswordRequestModel model)
        //{
        //    await _identityManager.ConfirmResetPasswordAsync(model);
        //    return Ok();
        //}

        ///// <summary>
        ///// Change Password from My Account Settings
        ///// </summary>
        //[Authorization]
        //[HttpPost("ChangePassword")]
        //[ProducesResponseType(typeof(SignInResponseModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequestModel model)
        //{
        //    UserContextModel userContextModel = _userContextService.GetUserContext();

        //    SignInResponseModel result = await _identityManager.ChangePasswordAsync(model, userContextModel);
        //    return Ok(result);
        //}

    }
}
