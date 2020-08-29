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

  public class AppointmentController : ControllerBase
  {
    private readonly IAppointmentRepository genericRepository;
    public AppointmentController(IAppointmentRepository _genericRepository)
    {
      genericRepository = _genericRepository;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Appointment>> GetAll()
    {
      return Ok(genericRepository.GetAll());
    }
    [HttpGet("GetAppointment/{AppointmentId}")]
    public ActionResult GetAppointmentById(int AppointmentId)
    {
      Appointment model = genericRepository.getAppointmentByID(AppointmentId);
      return Ok(model);
    }

    [HttpPut("EditAppointment/{id}")]
    public ActionResult EditAppointment([FromBody]Appointment model, int id)
    {
      var gModel = GetAppointmentById(id);
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

    [HttpDelete("Delete/{AppointmentId}")]
    public ActionResult Delete(int AppointmentId)
    {
      try
      {
        genericRepository.Delete(AppointmentId);

        return Ok(true);
      }
      catch
      {
        return BadRequest(false);
      }
    }
    [HttpPost("AddAppointment")]
    public async Task<ActionResult> AddAppointmentAsync([FromBody]Appointment model)
    {
      if (ModelState.IsValid)
      {
        await genericRepository.InsertAsync(model);

      }
      return Ok(true);
    }
  }
}