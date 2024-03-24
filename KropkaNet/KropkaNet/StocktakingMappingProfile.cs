using AutoMapper;
using KropkaNet.Models.Dtos.ClientSide.Address;
using KropkaNet.Models.Dtos.ClientSide.Company;
using KropkaNet.Models.Dtos.ClientSide.Department;
using KropkaNet.Models.Dtos.ClientSide.Order;
using KropkaNet.Models.Dtos.ClientSide.User;
using KropkaNet.Models.Objects.ClientSide;

namespace KropkaNet
{
    public class StocktakingMappingProfile : Profile
    {
        public StocktakingMappingProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>();

            CreateMap<Company, CompanyDto>();
            CreateMap<CreateCompanyDto, Company>();
            CreateMap<UpdateCompanyDto, Company>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();

            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
