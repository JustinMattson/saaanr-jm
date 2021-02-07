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
  public class ContactsController : ControllerBase
  {
    private readonly ContactsService _cs;
    public ContactsController(ContactsService cs)
    {
      _cs = cs;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Contact>> Get()
    {
      try
      {
        return Ok(_cs.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("user")]
    [Authorize]
    public ActionResult<IEnumerable<Contact>> GetContactsByUser()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_cs.GetContactsByUser(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    // // get keep by ID may be required to update the counts (keeps and views)
    // // ContactDetails?
    // [HttpGet("{id}")]
    // public ActionResult<Contact> GetContactById(int id)
    // {
    //   try
    //   {
    //     string userId = "";
    //     var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    //     if (claim != null)
    //     {
    //       userId = claim.Value;
    //     }
    //     return Ok(_cs.GetContactById(id, userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   };
    // }

    // [HttpPost]
    // [Authorize]
    // public ActionResult<Contact> Post([FromBody] Contact newContact)
    // {
    //   try
    //   {
    //     newContact.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_cs.Create(newContact));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    // // ContactToUpdate ktu
    // // This method should update keep.views onRouterEnter?
    // // This method should update keep.keeps when keep icon clicked
    // // This method should update isPrivate bool
    // [HttpPut("{id}")]
    // [Authorize]
    // public ActionResult<Contact> Edit(int id, [FromBody] Contact ktu)
    // {
    //   try
    //   {

    //     ktu.Id = id;
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_cs.Edit(ktu, userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    // // Delete only if keep is "private"
    // // Above comment fails to pass keepr-testing-tool
    // [HttpDelete("{id}")]
    // [Authorize]
    // public ActionResult<string> Delete(int id)
    // {
    //   try
    //   {
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_cs.Delete(id, userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
  }
}