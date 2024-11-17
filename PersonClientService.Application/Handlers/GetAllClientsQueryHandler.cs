using AutoMapper;
using MediatR;
using PersonClientService.Application.DTOs.Outputs;
using PersonClientService.Application.Query;
using PersonClientService.Core.Domain.Interfaces;

namespace PersonClientService.Application.Handlers
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientOutputDto>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public GetAllClientsQueryHandler(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientOutputDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return _mapper.Map<IEnumerable<ClientOutputDto>>(clients);
        }
    }
}
