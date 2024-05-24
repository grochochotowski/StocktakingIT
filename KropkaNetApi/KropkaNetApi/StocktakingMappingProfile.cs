using AutoMapper;
using KropkaNetApi.X_Models.ClientSide.Address;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.X_Models.ClientSide.User;
using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.CompanySide.Position;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;
using KropkaNetApi.X_Models.CompanySide.Warehouse;
using KropkaNetApi.X_Models.CompanySide.WarehouseProduct;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using System.Reflection.Emit;

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

            CreateMap<Order, OrderDto>();

            CreateMap<User, UserDto>();



            CreateMap<Employee, EmployeeDto>();

            CreateMap<Position, PositionDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Stocktaking, StocktakingDto>();

            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<WarehouseProduct, WarehouseProductDto>();

            CreateMap<CreateCompanyDto, Company>();
        }
    }
}
