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
  public class VaultsController : ControllerBase
  {
    private readonly VaultRepository _repo;
    public VaultsController(VaultRepository repo)
    {
      _repo = repo;
    }

    // GET api/vaults
    // [HttpGet]
    // public ActionResult<IEnumerable<Vault>> Get()
    // {
    //   try
    //   {
    //     return Ok(_repo.GetAllPublic());
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e);
    //   }
    // }

    // GET api/vaults/5
    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      try
      {
        return Ok(_repo.GetById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    //GET api/vaults/
    [Authorize]
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> GetByUser()
    {
      var user = HttpContext.User.FindFirstValue("Id");
      try
      {
        return Ok(_repo.GetByUserId(user));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // POST api/vaults
    [Authorize]
    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault value)
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

    // PUT api/vaults/5
    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<Vault> Put(int id, [FromBody] Vault value)
    {
      try
      {
        value.Id = id;
        return Ok(_repo.Update(value));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    // DELETE api/vaults/5
    [HttpDelete("{id}")]
    public ActionResult<String> Delete(int id)
    {
      try
      {
        return Ok(_repo.Delete(id));
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}