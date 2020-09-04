
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using Services.IServices;

namespace ClinicWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  public class ItemsController : ControllerBase
  {
        private IServicesItems ServicesItems;
        public ItemsController(IServicesItems _ServicesItems)
        {
            ServicesItems = _ServicesItems;
        }
        [HttpGet("GetAll")]
        public IResponseDTO GetAll()
        {
            var result = ServicesItems.GetAll();
            return result;
        }
        [HttpGet("GetItems/{ItemsId}")]
        public async Task<IResponseDTO> GetItemsByIdAsync(int ItemsId)
        {
            var result = await ServicesItems.GetByIdAsync(ItemsId);
            return result;
        }

        [HttpPut("EditItems")]
        public IResponseDTO EditItems([FromBody] ItemsViewModel model)
        {

            var result = ServicesItems.Update(model);
            return result;
        }

        [HttpDelete("ItemsDelete")]
        public IResponseDTO Delete([FromBody] ItemsViewModel model)
        {

            var result = ServicesItems.Delete(model);
            return result;
        }
        [HttpPost("AddItems")]
        public async Task<IResponseDTO> AddItemsAsync([FromBody] ItemsViewModel model)
        {
            var result = await ServicesItems.InsertAsync(model);
            return result;
        }
    }
}