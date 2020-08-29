using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Repository;
using DataAccessLayer;
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

  public class ClinicController : ControllerBase
  {
   
        private IServicesClinic ServicesClinic;
        public ClinicController(IServicesClinic _ServicesClinic)
             {
            ServicesClinic = _ServicesClinic;
        }
    [HttpGet("GetAll")]
    public IResponseDTO GetAll()
    {
            var result = ServicesClinic.GetAll();
      return result;
    }
        [HttpGet("GetClinic/{ClinicId}")]
        public async Task<IResponseDTO> GetClinicByIdAsync(int ClinicId)
        {
            var result = await ServicesClinic.GetByIdAsync(ClinicId);
            return result;
        }

        [HttpPut("EditClinic")]
        public IResponseDTO EditClinic([FromBody] ClinicViewModel model)
        {
           
          var result =  ServicesClinic.Update(model);
            return result;
        }

        [HttpDelete("ClinicDelete")]
        public IResponseDTO Delete([FromBody] ClinicViewModel  model)
        {
            
            var result = ServicesClinic.Delete(model);
            return result;
            }
        [HttpPost("AddClinic")]
        public async Task<IResponseDTO> AddClinicAsync([FromBody] ClinicViewModel model)
        {
            var result =await ServicesClinic.InsertAsync(model);
            return result;
        }
    }
}