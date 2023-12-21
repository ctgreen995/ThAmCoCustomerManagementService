using AutoMapper;
using CustomerManagementService.Data.Models;
using CustomerManagementService.Dtos;
using CustomerManagementService.Models;

namespace CustomerManagementService.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CustomerProfileDto, CustomerProfile>();
            CreateMap<CustomerProfile, CustomerProfileDto>();
            CreateMap<CustomerAccount, CustomerAccountDto>();
            CreateMap<CustomerAccountDto, CustomerAccount>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}