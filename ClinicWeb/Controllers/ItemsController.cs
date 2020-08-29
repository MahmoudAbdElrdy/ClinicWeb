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

  public class ItemsController : ControllerBase
  {
    private readonly IGenericRepositry<Items> genericRepository;
    public ItemsController(IGenericRepositry<Items> _genericRepository)
    {
      genericRepository = _genericRepository;
    }
    [HttpGet("GetAll")]
    public ActionResult<IEnumerable<Items>> GetAll()
    {
      return Ok(genericRepository.GetAll());
    }
    [HttpGet("GetItems/{ItemsId}")]
    public async Task<ActionResult> GetItemsByIdAsync(int ItemsId)
    {
      Items model = await genericRepository.GetByIdAsync(ItemsId);
      return Ok(model);
    }

    [HttpPut("EditItems/{id}")]
    public async Task<ActionResult> EditItemsAsync([FromBody]Items model, int id)
    {

      var gModel = await GetItemsByIdAsync(id);
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

    [HttpDelete("Delete/{ItemsId}")]
    public ActionResult Delete(int ItemsId)
    {
      try
      {
        genericRepository.Delete(ItemsId);

        return Ok(true);
      }
      catch
      {
        return BadRequest(false);
      }
    }
    [HttpPost("AddItems")]
    public async Task<ActionResult> AddItemsAsync([FromBody]Items model)
    {
      if (ModelState.IsValid)
      {
        await genericRepository.InsertAsync(model);

      }
      return Ok(true);
    }
  }
}