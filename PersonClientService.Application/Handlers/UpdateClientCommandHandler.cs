using AutoMapper;
using MediatR;
using PersonClientService.Application.Command;
using PersonClientService.Application.DTOs.Inputs;
using PersonClientService.Core.Domain.Entities;
using PersonClientService.Core.Domain.Interfaces;

namespace PersonClientService.Application.Handlers
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IPersonRepository personRepository, IClientRepository clientRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _personRepository = personRepository;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Buscar el cliente existente
                var client = await _clientRepository.GetClientByIdAsync(request.UpdateClientInputDto.ClientId);
                if (client == null) return false;

                // Buscar la persona asociada
                var person = await _personRepository.GetPersonByIdAsync(client.PersonId);
                if (person == null) return false;

                // Aplicar solo los cambios que están presentes en el DTO
                ApplyPartialUpdate(request.UpdateClientInputDto, client, person);

                // Actualizar las entidades
                await _personRepository.UpdatePersonAsync(person);
                await _clientRepository.UpdateClientAsync(client);

                // Confirmar la transacción
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception)
            {
                // Rollback en caso de error
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private void ApplyPartialUpdate(UpdateClientInputDto dto, Client client, Person person)
        {
            if (dto.Name != null) person.Name = dto.Name;
            if (dto.Gender.HasValue) person.Gender = dto.Gender.Value;
            if (dto.DateOfBirth.HasValue) person.DateOfBirth = dto.DateOfBirth.Value;
            if (dto.Identification != null) person.Identification = dto.Identification;
            if (dto.Address != null) person.Address = dto.Address;
            if (dto.Phone != null) person.Phone = dto.Phone;
            if (dto.Password != null) client.Password = dto.Password;
            if (dto.Status.HasValue) client.Status = dto.Status.Value;
        }
    }
}
