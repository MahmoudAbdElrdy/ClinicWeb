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

  public class PatientController : ControllerBase
  {
        private IServicesPatient ServicesPatient;
        public PatientController(IServicesPatient _ServicesPatient)
        {
            ServicesPatient = _ServicesPatient;
        }
        [HttpGet("GetAll")]
        public IResponseDTO GetAll()
        {
            var result = ServicesPatient.GetAll();
            return result;
        }
        [HttpGet("GetPatient/{PatientId}")]
        public async Task<IResponseDTO> GetPatientByIdAsync(int PatientId)
        {
            var result = await ServicesPatient.GetByIdAsync(PatientId);
            return result;
        }

        [HttpPut("EditPatient")]
        public IResponseDTO EditPatient([FromBody] PatientViewModel model)
        {

            var result = ServicesPatient.Update(model);
            return result;
        }

        [HttpDelete("PatientDelete")]
        public IResponseDTO Delete([FromBody] PatientViewModel model)
        {

            var result = ServicesPatient.Delete(model);
            return result;
        }
        [HttpPost("AddPatient")]
        public async Task<IResponseDTO> AddPatientAsync([FromBody] PatientViewModel model)
        {
            var result = await ServicesPatient.InsertAsync(model);
            return result;
        }
    }
}