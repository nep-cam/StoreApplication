using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.Store;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            this._storeService = storeService;
        }

        [HttpPost("create")]
        public IActionResult Create(CreateStoreDto input)
        {
            _storeService.Create(input);
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult Update(UpdateStoreDto input)
        {
            _storeService.Update(input);
            return Ok();
        }

        [HttpGet("search-store/{keyword}")]
        public IActionResult Search(string keyword, int page, int pageSize)
        {
            return Ok(_storeService.SearchStore(keyword, page, pageSize));
        }

        [HttpGet("get-suppliers/{storeId}")]
        public IActionResult GetSuppliers(int storeId, int page, int pageSize)
        {
            return Ok(_storeService.GetMaxSuppliers(storeId, page, pageSize));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _storeService.Delete(id);
            return Ok();
        }
    }
}
