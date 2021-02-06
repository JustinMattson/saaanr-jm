using System;
using System.Collections.Generic;
using System.Security.Claims;
using SaaaNR.Models;
using SaaaNR.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SaaaNR.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  // Everything related to vaults requires [Authorize] && userId validation
  [Authorize]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;
    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    // NOTE a basic GET ALL does not make sense since vaults are private, however, in order to get past the HTTP 405, will need to implement a basic get.  This "GetAll" will duplicate GetVaultsByUser functionality.
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.GetVaultsByUser(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // NOTE both this and the basic Get yield the same results with different route.  Need to ask senior which is preferred.
    // Get all vaults for authorized user
    // [HttpGet("user")]
    // public ActionResult<IEnumerable<Vault>> GetVaultsByUser()
    // {
    //   try
    //   {
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_vs.GetVaultsByUser(userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   };
    // }

    [HttpGet("{id}")]
    public ActionResult<Vault> GetVaultById(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.GetVaultById(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    // Get User DTO Keeps by VaultId
    [HttpGet("{vaultId}" + "/keeps")]
    public ActionResult<IEnumerable<VaultKeepViewModel>> GetDTOKeepsByVaultId(int vaultId)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vks.GetDTOKeepsByVaultId(vaultId, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault newVault)
    {
      try
      {
        newVault.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.Create(newVault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // VaultToUpdate(vtu)
    [HttpPut("{id}")]
    public ActionResult<Vault> Edit(int id, [FromBody] Vault vtu)
    {
      try
      {
        vtu.Id = id;
        vtu.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.Edit(vtu));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_vs.Delete(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}