using AutoMapper;
using KropkaNet.Objects.Entities.Models.ClientSide;
using KropkaNet.Objects.Entities.Models.CompanySide;
using KropkaNet.Objects.Dtos.ClientSide.Address;
using KropkaNet.Objects.Dtos.ClientSide.Company;
using KropkaNet.Objects.Dtos.ClientSide.Department;
using KropkaNet.Objects.Dtos.ClientSide.Order;
using KropkaNet.Objects.Dtos.ClientSide.User;
using KropkaNet.Objects.Dtos.CompanySide.Employee;
using KropkaNet.Objects.Dtos.CompanySide.Position;
using KropkaNet.Objects.Dtos.CompanySide.Product;
using KropkaNet.Objects.Dtos.CompanySide.Stocktaking;
using KropkaNet.Objects.Dtos.CompanySide.Warehouse;
using KropkaNet.Objects.Dtos.CompanySide.WarehouseProduct;

namespace KropkaNetApi
{
    public class StocktakingMappingProfile : Profile
    {
        public StocktakingMappingProfile()
        {
            CreateMap<Address, AddressDto>();

            CreateMap<Company, CompanyDto>();
            CreateMap<CreateCompanyDto, Company>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();

            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();



            CreateMap<Employee, EmployeeDto>();

            CreateMap<Position, PositionDto>(); 

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Stocktaking, StocktakingDto>();
            CreateMap<CreateStocktakingDto, Stocktaking>();

            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<WarehouseProduct, WarehouseProductDto>();
        }
    }
}
