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
  // Everything related to profiles requires [Authorize] && userId validation
  [Authorize]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    // NOTE a basic GET ALL does not make sense since profiles are private, however, in order to get past the HTTP 405, will need to implement a basic get.  This "GetAll" will duplicate GetProfilesByUser functionality.
    [HttpGet]

    public ActionResult<IEnumerable<Profile>> Get()
    {
      try
      {
        return Ok(_ps.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
    // public ActionResult<IEnumerable<Profile>> Get()
    // {
    //   try
    //   {
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_ps.GetProfilesByUser(userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    // NOTE both this and the basic Get yield the same results with different route.  Need to ask senior which is preferred.
    // Get all profiles for authorized user
    // [HttpGet("user")]
    // public ActionResult<IEnumerable<Profile>> GetProfilesByUser()
    // {
    //   try
    //   {
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_ps.GetProfilesByUser(userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   };
    // }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfileById(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ps.GetProfileById(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    // Get User DTO Keeps by ProfileId
    // [HttpGet("{profileId}" + "/keeps")]
    // public ActionResult<IEnumerable<ProfileKeepViewModel>> GetDTOKeepsByProfileId(int profileId)
    // {
    //   try
    //   {
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_vks.GetDTOKeepsByProfileId(profileId, userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpPost]
    public ActionResult<Profile> Post([FromBody] Profile newProfile)
    {
      try
      {
        newProfile.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ps.Create(newProfile));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // ProfileToUpdate(ptu)
    [HttpPut("{id}")]
    public ActionResult<Profile> Edit(int id, [FromBody] Profile ptu)
    {
      try
      {
        ptu.Id = id;
        ptu.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ps.Edit(ptu));
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
        return Ok(_ps.Delete(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}