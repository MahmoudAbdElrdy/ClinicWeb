using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using Services.IServices;

namespace ClinicWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  public class StoreController : ControllerBase
  {
        private IServicesStore ServicesStore;
        public StoreController(IServicesStore _ServicesStore)
        {
            ServicesStore = _ServicesStore;
        }
        [HttpGet("GetAll")]
        public IResponseDTO GetAll()
        {
            var result = ServicesStore.GetAll();
            return result;
        }
        [HttpGet("GetStore/{StoreId}")]
        public async Task<IResponseDTO> GetStoreByIdAsync(int StoreId)
        {
            var result = await ServicesStore.GetByIdAsync(StoreId);
            return result;
        }

        [HttpPut("EditStore")]
        public IResponseDTO EditStore([FromBody] StoreViewModel model)
        {

            var result = ServicesStore.Update(model);
            return result;
        }

        [HttpDelete("StoreDelete")]
        public IResponseDTO Delete([FromBody] StoreViewModel model)
        {

            var result = ServicesStore.Delete(model);
            return result;
        }
        [HttpPost("AddStore")]
        public async Task<IResponseDTO> AddStoreAsync([FromBody] StoreViewModel model)
        {
            var result = await ServicesStore.InsertAsync(model);
            return result;
        }
    }
}