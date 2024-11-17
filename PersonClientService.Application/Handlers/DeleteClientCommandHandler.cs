using MediatR;
using PersonClientService.Application.Command;
using PersonClientService.Core.Domain.Interfaces;

namespace PersonClientService.Application.Handlers
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IClientRepository clientRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(request.ClientId);
                if (client == null) return false;

                await _clientRepository.DeleteClientAsync(client);

                var person = await _personRepository.GetPersonByIdAsync(client.PersonId);
                if (person != null)
                {
                    await _personRepository.DeletePersonAsync(person);
                }

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
    }
}
