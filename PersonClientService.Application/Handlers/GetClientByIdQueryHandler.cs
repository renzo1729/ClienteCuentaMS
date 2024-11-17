using AutoMapper;
using MediatR;
using PersonClientService.Application.DTOs.Outputs;
using PersonClientService.Application.Query;
using PersonClientService.Core.Domain.Interfaces;

namespace PersonClientService.Application.Handlers
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientOutputDto>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetClientByIdQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientOutputDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByIdAsync(request.ClientId);
            return _mapper.Map<ClientOutputDto>(client);
        }
    }
}
