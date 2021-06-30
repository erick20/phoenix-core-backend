using Identity.Application.Exceptions;
using Identity.Application.Features.RoleGroup.V1;
using Identity.Application.Features.RoleGroup.V1.Commands.CreateRoleGroup;
using Identity.Application.Features.RoleGroup.V1.Commands.UpdateRoleGroup;
using Identity.Application.Features.RoleGroup.V1.Queries.GetRoleGroupList;
using Identity.Application.Features.RoleGroup.V1.Queries.GetRolesGroupById;
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
        /// Get RoleGroup List
        /// </summary>
        //[Authorization]
        [HttpGet("RoleGroups")]
        [ProducesResponseType(typeof(List<RoleGroupV1Response>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoleGroupList()
        {
            var query = new GetRoleGroupListV1Query();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Get  RoleGroup by id
        /// </summary>
        //[Authorization]
        [HttpGet("RoleGroupsById")]
        [ProducesResponseType(typeof(List<RoleGroupV1Response>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoleGroup(int id)
        {
            var query = new GetRoleGroupByIdV1Query();            
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Create Role Group
        /// </summary>
        [HttpPost]
        //[Authorization]
        [ProducesResponseType(typeof(RoleGroupV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRoleGroup([FromBody] CreateRoleGroupV1Command request)
        {
            RoleGroupV1Response result = await _mediator.Send(request);

            return Ok(result);
        }

        /// <summary>
        /// Update Role Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //   [Authorization]
        [ProducesResponseType(typeof(RoleGroupV1Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ErrorValidationModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateRoleGroup(int id, [FromBody] UpdateRoleGroupV1Command request) // todo [ValidationRequired]
        {
            request.Id = id;
            RoleGroupV1Response result = await _mediator.Send(request);

            return Ok(result);
        }

    }
}
