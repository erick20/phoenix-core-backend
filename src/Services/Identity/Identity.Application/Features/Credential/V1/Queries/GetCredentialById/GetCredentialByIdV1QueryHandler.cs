using AutoMapper;
using Identity.Application.Contracts.Persistence.UnitOfWork;
using Identity.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Credential.V1.Queries.GetCredentialById
{
    public class GetCredentialByIdV1QueryHandler : IRequestHandler<GetCredentialByIdV1Query, CredentialV1Response>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCredentialByIdV1QueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public GetCredentialByIdV1QueryHandler(IMapper mapper, ILogger<GetCredentialByIdV1QueryHandler> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<CredentialV1Response> Handle(GetCredentialByIdV1Query request, CancellationToken cancellationToken)
        {
            var credential = await _unitOfWork.CredentialRepositoryV1.GetCredetialByIdAsync(request.Id, false);

            CredentialV1Response response = _mapper.Map<CredentialV1Response>(credential);

            return response;
        }
    }
}
