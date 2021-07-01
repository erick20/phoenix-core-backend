using Identity.Application.Exceptions;
using Identity.Application.Features.PermissionGroup;
using Identity.Application.Features.PermissionGroup.V1.Commands.CreatePermissionGroup;
using Identity.Application.Features.PermissionGroup.V1.Commands.UpdatePermissionGroup;
using Identity.Application.Features.PermissionGroup.V1.Queries.GetPermissionGroupList;
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
    public class PermissionGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionGroupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get PermissionGroup List
        /// </summary>
        //[Authorization]
        [HttpGet("PermissionGroups")]
        [ProducesResponseType(typeof(List<PermissionGroupV1Response>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPermissionGroupList()
        {
            var query = new GetPermissionGroupListV1Query();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        ///// <summary>
        ///// Get  RoleGroup by id
        ///// </summary>
        ////[Authorization]
        //[HttpGet("RoleGroupsById")]
        //[ProducesResponseType(typeof(List<PermissionGroupV1Response>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetRoleGroupById(int id)
        //{
        //    var query = new GetPermissionGroupByIdV1Query { };            
        //    var result = await _mediator.Send(query);

        //    return Ok(result);
        //}

        /// <summary>
        /// Create Permission Group
        /// </summary>
        [HttpPost]
        //[Authorization]
        [ProducesResponseType(typeof(PermissionGroupV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePermissionGroup([FromBody] CreatePermissionGroupV1Command request)
        {
            PermissionGroupV1Response result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Update Permission Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //   [Authorization]
        [ProducesResponseType(typeof(PermissionGroupV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePermissionGroup(int id, [FromBody] UpdatePermissionGroupV1Command request) // todo [ValidationRequired]
        {
            request.Id = id;
            PermissionGroupV1Response result = await _mediator.Send(request);

            return Ok(result);
        }

    }
}
