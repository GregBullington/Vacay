using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vacay.Models;
using Vacay.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vacay.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
      _accountService = accountService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    [Authorize]

    public async Task<ActionResult<Account>> EditAsync([FromBody] Account editedAccount)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Account account = _accountService.Edit(editedAccount, userInfo.Id);
        return Ok(editedAccount);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}