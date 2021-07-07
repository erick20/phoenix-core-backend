using AutoMapper;
using Identity.Application.Features.PermissionGroup;
using Identity.Application.Features.PermissionGroup.V1.Commands.CreatePermissionGroup;
using Identity.Domain.Entities;

namespace Identity.Application.Mappings
{
    public class PermissionGroupProfile : Profile
    {
        public PermissionGroupProfile()
        {
            #region To Entity

            CreateMap<CreatePermissionGroupV1Command, PermissionGroup>();

            #endregion

            #region From Entity

            CreateMap<PermissionGroup, PermissionGroupV1Response>();

            #endregion
        }
    }
}
