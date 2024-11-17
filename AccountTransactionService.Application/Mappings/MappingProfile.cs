using AccountTransactionService.Application.DTOs.Inputs;
using AccountTransactionService.Application.DTOs.Outputs;
using AccountTransactionService.Core.Domain.Entities;
using AutoMapper;

namespace AccountTransactionService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapear de CreateAccountInputDto a Account
            CreateMap<CreateAccountInputDto, Account>();

            // Mapear de Account a AccountOutputDto
            CreateMap<Account, AccountOutputDto>();

            CreateMap<TransactionRecord, TransactionRecordOutputDto>().ReverseMap();
        }
    }
}
