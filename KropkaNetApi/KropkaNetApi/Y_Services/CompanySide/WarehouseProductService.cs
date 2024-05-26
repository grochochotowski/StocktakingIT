using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.CompanySide.Product;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IWarehouseProductService
    {
        ReturnResult<ProductListDto> GetFromWarehouse(int warehouseId, int page, string filter, string sortBy, SortDirection sortDireciton);
        void AddProduct(int warehouseId, int productId, int quantity);
        void RemoveProduct(int warehouseId, int productId, int quantity);
        void EditQuantity(int warehouseId, int productId, int quantity);
    }
    public class WarehouseProductService
    {

    }
}
