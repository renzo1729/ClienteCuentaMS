using AutoMapper;
using MediatR;
using PersonClientService.Application.Command;
using PersonClientService.Core.Domain.Entities;
using PersonClientService.Core.Domain.Interfaces;
using PersonClientService.Core.Shared.Exceptions;

namespace PersonClientService.Application.Handlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IPersonRepository personRepository, IClientRepository clientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personRepository = personRepository;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var existingPerson = await _personRepository.GetByIdentificationAsync(request.CreateClientInputDto.Identification);
                if (existingPerson != null)
                {
                    throw new ValidationException($"La identificación '{request.CreateClientInputDto.Identification}' ya existe.");
                }
                // Crear la entidad Person y guardarla en la base de datos
                var person = _mapper.Map<Person>(request.CreateClientInputDto);
                await _personRepository.AddPersonAsync(person);
                // Crear la entidad Client
                var client = new Client
                {
                    PersonId = person.PersonId,
                    Password = request.CreateClientInputDto.Password,
                    Status = true // Por defecto, el estado es activo
                };

                await _clientRepository.AddClientAsync(client);

                // Confirmar la transacción
                await _unitOfWork.CommitTransactionAsync();

                return client.ClientId;
            }
            catch (Exception)
            {
                // Rollback en caso de error
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
