using AccountTransactionService.Core.Domain.DTOs.External;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Response;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AccountTransactionService.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ClientService:BaseUrl"] ?? throw new Exception("Configure URL para servicio Cliente"); ;
        }

        public async Task<ClientDto> GetClientByIdAsync(int clientId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Client/{clientId}");
            response.EnsureSuccessStatusCode();

            var clientData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<ClientDto>>(clientData,options);

            if (apiResponse != null && apiResponse.Success)
            {
                return apiResponse.Data;
            }
            else
            {
                throw new Exception("Client retrieval was not successful.");
            }
        }
    }
}
