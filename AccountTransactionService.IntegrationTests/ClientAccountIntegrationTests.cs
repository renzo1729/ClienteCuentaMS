namespace AccountTransactionService.IntegrationTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;
    using Newtonsoft.Json;
    using AccountTransactionService.Core.Domain.DTOs.External;
    using AccountTransactionService.Core.Shared.Response;

    public class ClientAccountIntegrationTests
    {
        private readonly HttpClient _client;

        public ClientAccountIntegrationTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://clientservice:5001")
            };
        }

        [Fact]
        public async Task GetClientById_ShouldReturnClientDetails()
        {
            var clientId = 1;
            var response = await _client.GetAsync($"/api/Client/{clientId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var clientResponse = JsonConvert.DeserializeObject<ApiResponse<ClientDto>>(content);

            clientResponse.Should().NotBeNull();
            clientResponse.Success.Should().BeTrue();
            clientResponse.Data.Should().NotBeNull();
            clientResponse.Data.ClientId.Should().Be(clientId);
        }
    }
}