using Xunit;
using FluentAssertions;
using PersonClientService.Core.Domain.Entities;

namespace PersonClientService.Tests
{
    public class ClientTests
    {
        [Fact]
        public void CreateClient_ShouldHaveCorrectProperties()
        {
            // Arrange
            var person = new Person
            {
                PersonId = 1,
                Name = "Renzo Cachay",
                Gender = 'M',
                DateOfBirth = new DateTime(1990, 1, 1),
                Identification = "46839167",
                Address = "Lambayeque",
                Phone = "951038962"
            };

            var client = new Client
            {
                ClientId = 1,
                PersonId = person.PersonId,
                Password = "1234",
                Status = true,
                Person = person
            };

            // Act & Assert
            client.ClientId.Should().Be(1);
            client.PersonId.Should().Be(1);
            client.Password.Should().Be("1234");
            client.Status.Should().BeTrue();
            client.Person.Should().NotBeNull();
            client.Person.Name.Should().Be("Renzo Cachay");
            client.Person.Identification.Should().Be("46839167");
        }
    }
}