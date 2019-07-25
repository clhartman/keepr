using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepRepository _repo;
    public VaultKeepsController(VaultKeepRepository repo)
    {
      _repo = repo;
    }

    // GET api/vaultkeeps/5
    [Authorize]
    [HttpGet("{vaultId}")]
    public ActionResult<IEnumerable<Keep>> Get(int vaultId)
    {
      var userId = HttpContext.User.FindFirstValue("Id");
      var keeps = _repo.GetByVaultId(vaultId, userId);
      if (keeps != null)
      {
        return Ok(keeps);
      }
      else
      {
        return BadRequest();
      }
    }

    // POST api/vaultkeeps
    [Authorize]
    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep value)
    {
      var userId = HttpContext.User.FindFirstValue("Id");
      value.UserId = userId;
      if (userId != null)
      {
        return Ok(_repo.Create(value));
      }
      else
      {
        return BadRequest();
      }
    }

    // PUT api/vaultkeeps/
    [Authorize]
    [HttpPut]
    public ActionResult<string> Put([FromBody] VaultKeep value)
    {
      var userId = HttpContext.User.FindFirstValue("Id");
      value.UserId = userId;
      try
      {
        return Ok(_repo.Delete(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // // DELETE api/vaultkeeps/
    // [HttpPut]
    // public ActionResult<String> Put(int id)
    // {
    //   try
    //   {
    //     return Ok(_repo.Put(id));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e);
    //   }
    // }
  }
}