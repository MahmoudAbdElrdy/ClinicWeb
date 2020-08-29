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

  public class PatientController : ControllerBase
  {
    private readonly IGenericRepositry<Patient> genericRepository;
    public PatientController(IGenericRepositry<Patient> _genericRepository)
    {
      genericRepository = _genericRepository;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Patient>> GetAll()
    {
      return Ok(genericRepository.GetAll());
    }
    [HttpGet("GetPatient/{PatientId}")]
    public async Task<ActionResult> GetPatientByIdAsync(int PatientId)
    {
      Patient model = await genericRepository.GetByIdAsync(PatientId);
      return Ok(model);
    }

    [HttpPut("EditPatient/{id}")]
    public async Task<ActionResult> EditPatientAsync([FromBody]Patient model, int id)
    {
      var gModel = await GetPatientByIdAsync(id);

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

    [HttpDelete("Delete/{PatientId}")]
    public ActionResult Delete(int PatientId)
    {
      try
      {
        genericRepository.Delete(PatientId);

        return Ok(true);
      }
      catch
      {
        return BadRequest(false);
      }
    }
    [HttpPost("AddPatient")]
    public async Task<ActionResult> AddPatientAsync([FromBody]Patient model)
    {
      if (ModelState.IsValid)
      {
        await genericRepository.InsertAsync(model);

      }
      return Ok(true);
    }
  }
}