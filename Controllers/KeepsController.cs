using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
    [HttpGet("user")]
    [Authorize]
    public ActionResult<IEnumerable<Keep>> GetKeepsByUser()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.GetKeepsByUser(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    // get keep by ID may be required to update the counts (keeps and views)
    // KeepDetails?
    [HttpGet("{id}")]
    public ActionResult<Keep> GetKeepById(int id)
    {
      try
      {
        string userId = "";
        var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim != null)
        {
          userId = claim.Value;
        }
        return Ok(_ks.GetKeepById(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        newKeep.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // KeepToUpdate ktu
    // This method should update keep.views onRouterEnter?
    // This method should update keep.keeps when keep icon clicked
    // This method should update isPrivate bool
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Keep> Edit(int id, [FromBody] Keep ktu)
    {
      try
      {

        ktu.Id = id;
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.Edit(ktu, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Delete only if keep is "private"
    // Above comment fails to pass keepr-testing-tool
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ks.Delete(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}