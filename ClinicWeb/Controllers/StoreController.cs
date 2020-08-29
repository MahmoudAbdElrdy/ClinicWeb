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

  public class StoreController : ControllerBase
  {
    private readonly IGenericRepositry<Store> genericRepository;
    public StoreController(IGenericRepositry<Store> _genericRepository)
    {
      genericRepository = _genericRepository;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Store>> GetAll()
    {
      return Ok(genericRepository.GetAll());
    }
    [HttpGet("GetStore/{StoreId}")]
    public async Task<ActionResult> GetStoreByIdAsync(int StoreId)
    {
      Store model = await genericRepository.GetByIdAsync(StoreId);
      return Ok(model);
    }

    [HttpPut("EditStore/{id}")]
    public async Task<ActionResult> EditStoreAsync([FromBody]Store model, int id)
    {
      var gModel = await GetStoreByIdAsync(id);

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

    [HttpDelete("Delete/{StoreId}")]
    public ActionResult Delete(int StoreId)
    {
      try
      {
        genericRepository.Delete(StoreId);

        return Ok(true);
      }
      catch
      {
        return BadRequest(false);
      }
    }
    [HttpPost("AddStore")]
    public async Task<ActionResult> AddStoreAsync([FromBody]Store model)
    {
      if (ModelState.IsValid)
      {
        await genericRepository.InsertAsync(model);
      }
      return Ok(true);
    }
  }
}