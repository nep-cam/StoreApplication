using WebApplication1.Dtos;
using WebApplication1.Dtos.Store;
using WebApplication1.Dtos.Supplier;
using WebApplication1.Entities;

namespace WebApplication1.Services.Interfaces
{
    public interface IStoreService
    {
        void Create(CreateStoreDto input);
        void Update(UpdateStoreDto input);
        void Delete(int id);
        PagedResult<StoreDto> SearchStore(string keyword, int page, int pageSize);
        PagedResult<SupplierDto> GetMaxSuppliers(int StoreId, int page, int pageSize);
    }
}
