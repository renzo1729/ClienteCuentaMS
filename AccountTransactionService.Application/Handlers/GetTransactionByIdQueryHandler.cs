using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Application.Query;
using AccountTransactionService.Core.Domain.Interfaces;
using AccountTransactionService.Core.Shared.Exceptions;
using AutoMapper;
using MediatR;

namespace AccountTransactionService.Application.Handlers
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionRecordOutputDto>
    {
        private readonly ITransactionRecordRepository _transactionRecordRepository;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(ITransactionRecordRepository transactionRecordRepository, IMapper mapper)
        {
            _transactionRecordRepository = transactionRecordRepository;
            _mapper = mapper;
        }

        public async Task<TransactionRecordOutputDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRecordRepository.GetTransactionRecordByIdAsync(request.TransactionId);
            return transaction == null
                ? throw new NotFoundException($"Transacción con ID {request.TransactionId} no encontrada.")
                : _mapper.Map<TransactionRecordOutputDto>(transaction);
        }
    }
}
