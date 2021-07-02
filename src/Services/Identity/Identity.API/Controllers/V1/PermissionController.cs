using Identity.API.Attributes;
using Identity.Application.Contracts.Infrastructure.Services;
using Identity.Application.Exceptions;
using Identity.Application.Features.Permission.V1;
using Identity.Application.Features.Permission.V1.Commands.CreatePermission;
using Identity.Application.Features.Permission.V1.Commands.UpdatePermission;
using Identity.Application.Features.Permission.V1.Queries.GetPermissionKeyListByRoleId;
using Identity.Application.Features.Permission.V1.Queries.GetPermissionList;
using Identity.Application.Features.Permission.V1.Queries.GetPermissionListByGroupId;
using Identity.Application.Features.Permission.V1.Queries.GetPermissionListByRoleAndGroupId;
using Identity.Application.Features.Permission.V1.Queries.GetPermissionListByRoleId;
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
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserContextService _userContextService;
        public PermissionController(IMediator mediator, IUserContextService userContextService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        #region Get

        /// <summary>
        /// Get Permission List
        /// </summary>
        //[Authorization]
        [HttpGet("Permissions")]
        [ProducesResponseType(typeof(List<PermissionV1Response>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPermissionList()
        {
            var query = new GetPermissionListV1Query();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Get Permissions List By Role Id
        /// </summary>
        ////[Authorization]
        [HttpGet("/Api/RolePermissions")]
        [Authorization]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPermissionKeysListByRoleId()
        {
            //await InternalPermissionHelper.Validate(_userContextService, _roleManager);

            var query = new GetPermissionKeyListByRoleIdV1Query { RoleId = _userContextService.GetUserContext().RoleId };
            List<string> result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Get Permission List By Role
        /// </summary>
        [HttpGet("/Api/Permissions/{roleId}")]
        [Authorization]
        [ProducesResponseType(typeof(List<PermissionV1Response>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPermissionListByRoleId(int roleId)
        {
            var query = new GetPermissionListByRoleIdV1Query { RoleId = roleId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Get Permission List By Group
        /// </summary>
        [HttpGet("/Api/Permissions/Group/{groupId}")]
        [Authorization]
        [ProducesResponseType(typeof(List<PermissionV1Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPermissionListByGroupId(int groupId) // [ValidationRequired] 
        {
            //await InternalPermissionHelper.Validate(_userContextService, _roleManager);

            var query = new GetPermissionListByGroupIdV1Query { GroupId = groupId };
            List<PermissionV1Response> result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Get Permission List By Group And Role
        /// </summary>
        [HttpGet("/Api/Permissions/{groupId}/{roleId}")]
        [Authorization]
        [ProducesResponseType(typeof(List<HasPermissionV1Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPermissionListByGroupAndRoleId(int groupId, int roleId)
        //[ValidationRequired] int groupId, [ValidationRequired] int roleId)
        {
            //await InternalPermissionHelper.Validate(_userContextService, _roleManager);
            var query = new GetPermissionListByRoleAndGroupIdV1Query { RoleId = roleId, GroupId = groupId };
            List<HasPermissionV1Response> result = await _mediator.Send(query);

            return Ok(result);
        }


        //[HttpGet("/Api/HasPermissions")] // not needed endpoint for frontend
        //[Authorization]
        //[ProducesResponseType(typeof(Dictionary<string, bool>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ValidationMessageModel>), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> HasPermissions([FromBody] List<string> permissions)
        //{
        //    await InternalPermissionHelper.Validate(_userContextService, _roleManager);

        //    Dictionary<string, bool> result = await _roleManager.HasPermissions(permissions, _userContextService.GetUserContext().RoleId);

        //    return Ok(result);
        //}



        #endregion

        #region Create Or Update

        [HttpPost]
        [Authorization]
        [ProducesResponseType(typeof(PermissionV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionV1Command request)
        {
            //await InternalPermissionHelper.Validate(_userContextService, _roleManager);

            PermissionV1Response result = await _mediator.Send(request);

            return Ok(result);
        }


        [HttpPut("{id}")]
        [Authorization]
        [ProducesResponseType(typeof(PermissionV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] UpdatePermissionV1Command request)//[ValidationRequired] 
        {
            //await InternalPermissionHelper.Validate(_userContextService, _roleManager);
            request.Id = id;
            PermissionV1Response result = await _mediator.Send(request);

            return Ok(result);
        }


        //[HttpDelete("{id}")]
        //[Authorization]
        //[ProducesResponseType(typeof(PermissionResponseModel), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(List<ValidationMessageModel>), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> DeletePermission([ValidationRequired] int id)
        //{
        //    await InternalPermissionHelper.Validate(_userContextService, _roleManager);

        //    int result = await _roleManager.DeletePermission(id);

        //    return Ok(result);
        //}

        #endregion
    }
}
