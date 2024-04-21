using AutoMapper;
using KropkaNet.Models.Dtos.ClientSide.Address;
using KropkaNet.Models.Dtos.ClientSide.Company;
using KropkaNet.Models.Dtos.ClientSide.Department;
using KropkaNet.Models.Dtos.ClientSide.Order;
using KropkaNet.Models.Dtos.ClientSide.User;
using KropkaNet.Models.Objects.ClientSide;
using KropkaNet.Models.Dtos.CompanySide.Employee;
using KropkaNet.Models.Dtos.CompanySide.Position;
using KropkaNet.Models.Dtos.CompanySide.Product;
using KropkaNet.Models.Dtos.CompanySide.Stocktaking;
using KropkaNet.Models.Dtos.CompanySide.Warehouse;
using KropkaNet.Models.Dtos.CompanySide.WarehouseProduct;
using KropkaNet.Models.Objects.CompanySide;

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

            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();


            CreateMap<Position, PositionDto>();


            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();


            CreateMap<Stocktaking, StocktakingDto>();
            CreateMap<CreateStocktakingDto, Stocktaking>();
            CreateMap<UpdateStocktakingDto, Stocktaking>();


            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<CreateWarehouseDto, Warehouse>();
            CreateMap<UpdateWarehouseDto, Warehouse>();


            CreateMap<WarehouseProduct, WarehouseProductDto>();
            CreateMap<CreateWarehouseProductDto, WarehouseProduct>();
            CreateMap<UpdateWarehouseProductDto, WarehouseProduct>();

        }
    }
}
