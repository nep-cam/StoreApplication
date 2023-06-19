using WebApplication1.Dtos.Store;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Services.Interfaces;
using WebApplication1.DbContexts;
using WebApplication1.Exceptions;
using System.Net;
using WebApplication1.Dtos.Supplier;

namespace WebApplication1.Services.Implements
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationContext _context;
        public StoreService(ApplicationContext context)
        {
            _context = context;
        }
        public void Create(CreateStoreDto input)
        {
            if (_context.Stores.FirstOrDefault(s => s.Name == input.Name) != null)
            {
                throw new UserFriendlyException($"Ten store da ton tai");
            }
            _context.Stores.Add(new Store
            {
                Name = input.Name,
                Address = input.Address,
                OpenTime = input.OpenTime,
                CloseTime = input.CloseTime

            });
            _context.SaveChanges();
        }
        public void Update(UpdateStoreDto input)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == input.Id);

            if (store == null)
            {
                throw new UserFriendlyException($"Store khong ton tai");
            }
            if (_context.Stores.FirstOrDefault(s => s.Name == input.Name) != null)
            {
                throw new UserFriendlyException($"Ten store da ton tai");
            }
            store.Name = input.Name;
            store.Address = input.Address;
            store.OpenTime = input.OpenTime;
            store.CloseTime = input.CloseTime;
            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);

            if (store == null)
            {
                throw new UserFriendlyException($"Store khong ton tai");
            }
            _context.Stores.Remove(store);
            _context.SaveChanges();
        }


        public PagedResult<StoreDto> SearchStore(string keyword, int page, int pageSize)
        {
            var query = _context.Stores.Where(s => s.Name.Contains(keyword) || s.Address.Contains(keyword))
                .Select(s => new StoreDto
                {
                    Name = s.Name,
                    Address = s.Address,
                    OpenTime = s.OpenTime,
                    CloseTime = s.CloseTime,
                });
            var storeDtos = query.ToList();
            var totalItems = query.Count();
            var result = new PagedResult<StoreDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = storeDtos
            };
            return result;
        }



        public PagedResult<SupplierDto> GetMaxSuppliers(int StoreId, int page, int pageSize)
        {
            var maxPoint = _context.StoreSuppliers.Where(s => s.StoreId == StoreId).Max(s => s.Point);
            var query = _context.StoreSuppliers.Join(_context.Suppliers, ss => ss.SupplierId, s => s.Id,
                (storeSupplier, supplier) => new
                {
                    storeSupplier,
                    supplier
                })
                .OrderBy(s => s.storeSupplier.Point)
                .Where(s => s.storeSupplier.StoreId == StoreId && s.storeSupplier.Point == maxPoint)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(s => new SupplierDto
                {
                    Name = s.supplier.Name,
                    Phone = s.supplier.Phone,
                });
            var supplierDtos = query.ToList();
            var totalItems = query.Count();
            var result = new PagedResult<SupplierDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Items = supplierDtos
            };
            return result;

        }
    }
}
