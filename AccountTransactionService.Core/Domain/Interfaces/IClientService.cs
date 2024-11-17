using AccountTransactionService.Core.Domain.DTOs.External;

namespace AccountTransactionService.Core.Domain.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> GetClientByIdAsync(int clientId);
    }
}
