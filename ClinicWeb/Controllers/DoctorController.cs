using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

  public class DoctorController : ControllerBase
  {
    private readonly IGenericRepositry<Doctor> genericRepository;
    public DoctorController(IGenericRepositry<Doctor> _genericRepository)
    {
      genericRepository = _genericRepository;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Doctor>> GetAll()
    {
      return Ok(genericRepository.GetAll());
    }
    [HttpGet("GetDoctor/{DoctorId}")]
    public async Task<ActionResult> GetDoctorByIdAsync(int DoctorId)
    {
      Doctor model = await genericRepository.GetByIdAsync(DoctorId);
      return Ok(model);
    }

    [HttpPut("EditDoctor/{id}")]
    public async Task<ActionResult> EditDoctorAsync([FromBody]Doctor model, int id)
    {

      var gModel = await GetDoctorByIdAsync(id);
      if (ModelState.IsValid && gModel != null)
      {
        genericRepository.Update(model);
        return Ok(model);
      }
      else
      {
        return BadRequest();
      }
    }

    [HttpDelete("Delete/{DoctorId}")]
    public ActionResult Delete(int DoctorId)
    {
      try
      {
        genericRepository.Delete(DoctorId);

        return Ok(true);
      }
      catch
      {
        return BadRequest(false);
      }
    }
    [HttpPost("AddDoctor")]
    public async Task<ActionResult> AddDoctorAsync([FromBody]Doctor model)
    {
      if (ModelState.IsValid)
      {
        await genericRepository.InsertAsync(model);

      }
      return Ok(true);
    }
  }
}