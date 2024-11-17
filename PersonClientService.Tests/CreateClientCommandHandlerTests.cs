using Xunit;
using FluentAssertions;
using PersonClientService.Core.Domain.Entities;
using PersonClientService.Core.Domain.Interfaces;
using PersonClientService.Application.Handlers;
using AutoMapper;
using PersonClientService.Application.DTOs.Inputs;
using PersonClientService.Application.Command;
using PersonClientService.Core.Shared.Exceptions;
using Moq;

namespace PersonClientService.Tests
{
    public class CreateClientCommandHandlerTests
    {
        private readonly Mock<IPersonRepository> _personRepositoryMock;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly CreateClientCommandHandler _handler;

        public CreateClientCommandHandlerTests()
        {
            _personRepositoryMock = new Mock<IPersonRepository>();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateClientInputDto, Person>();
            });
            _mapper = configuration.CreateMapper();

            _handler = new CreateClientCommandHandler(
                _personRepositoryMock.Object,
                _clientRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateClient_WhenDataIsValid()
        {
            // Arrange
            var inputDto = new CreateClientInputDto
            {
                Name = "Renzo Cachay",
                Gender = 'M',
                DateOfBirth = new DateTime(1992, 2, 17),
                Identification = "46839167",
                Address = "Lambayeque",
                Phone = "951038962",
                Password = "1234"
            };
            var command = new CreateClientCommand(inputDto);

            _personRepositoryMock.Setup(repo => repo.GetByIdentificationAsync(It.IsAny<string>()))
                .ReturnsAsync(null as Person);

            _personRepositoryMock.Setup(repo => repo.AddPersonAsync(It.IsAny<Person>()))
                .Returns(Task.CompletedTask);

            _clientRepositoryMock.Setup(repo => repo.AddClientAsync(It.IsAny<Client>()))
                .Callback<Client>(c => c.ClientId = 1)
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(uow => uow.BeginTransactionAsync())
                .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(uow => uow.CommitTransactionAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);
            _personRepositoryMock.Verify(repo => repo.AddPersonAsync(It.IsAny<Person>()), Times.Once);
            _clientRepositoryMock.Verify(repo => repo.AddClientAsync(It.IsAny<Client>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenIdentificationExists()
        {
            // Arrange
            var inputDto = new CreateClientInputDto
            {
                Name = "Renzo Cachay",
                Gender = 'M',
                DateOfBirth = new DateTime(1992, 2, 2),
                Identification = "46839167",
                Address = "Lambayeque",
                Phone = "951038962",
                Password = "1234"
            };
            var command = new CreateClientCommand(inputDto);

            _personRepositoryMock.Setup(repo => repo.GetByIdentificationAsync(It.IsAny<string>()))
                .ReturnsAsync(new Person { Identification = "46839167" });

            // Act
            Func<Task> act = async () => { await _handler.Handle(command, CancellationToken.None); };

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("La identificación '46839167' ya existe.");
            _personRepositoryMock.Verify(repo => repo.AddPersonAsync(It.IsAny<Person>()), Times.Never);
            _clientRepositoryMock.Verify(repo => repo.AddClientAsync(It.IsAny<Client>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.RollbackTransactionAsync(), Times.Once);
        }
    }

}
