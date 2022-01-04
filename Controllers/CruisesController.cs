using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vacay.Models;
using Vacay.Services;

namespace Vacay.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class CruisesController : ControllerBase
  {
    private readonly CruisesService _cs;

    public CruisesController(CruisesService cs)
    {
      _cs = cs;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Cruise>> Get()
    {
      try
      {
        var cruises = _cs.Get();
        return Ok(cruises);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]

    public ActionResult<Cruise> GetById(int id)
    {
      try
      {
        var cruise = _cs.GetById(id);
        return Ok(cruise);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Cruise>> Create([FromBody] Cruise newCruise)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newCruise.CreatorId = userInfo?.Id;
        Cruise cruise = _cs.Create(newCruise);
        return Ok(cruise);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Cruise>> EditAsync([FromBody] Cruise editedCruise, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        editedCruise.Id = id;
        Cruise cruise = _cs.Edit(editedCruise, userInfo.Id);
        return Ok(editedCruise);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _cs.Remove(id, userInfo.Id);
        return Ok("Cruise has been Deleted!");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}