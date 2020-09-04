
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.DTO;
using Services.IServices;

namespace DoctorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  public class DoctorController : ControllerBase
  {
        private IServicesDoctor ServicesDoctor;
        public DoctorController(IServicesDoctor _ServicesDoctor)
        {
            ServicesDoctor = _ServicesDoctor;
        }
        [HttpGet("GetAll")]
        public IResponseDTO GetAll()
        {
            var result = ServicesDoctor.GetAll();
            return result;
        }
        [HttpGet("GetDoctor/{DoctorId}")]
        public async Task<IResponseDTO> GetDoctorByIdAsync(int DoctorId)
        {
            var result = await ServicesDoctor.GetByIdAsync(DoctorId);
            return result;
        }

        [HttpPut("EditDoctor")]
        public IResponseDTO EditDoctor([FromBody] DoctorViewModel model)
        {

            var result = ServicesDoctor.Update(model);
            return result;
        }

        [HttpDelete("DoctorDelete")]
        public IResponseDTO Delete([FromBody] DoctorViewModel model)
        {

            var result = ServicesDoctor.Delete(model);
            return result;
        }
        [HttpPost("AddDoctor")]
        public async Task<IResponseDTO> AddDoctorAsync([FromBody] DoctorViewModel model)
        {
            var result = await ServicesDoctor.InsertAsync(model);
            return result;
        }
    }
}