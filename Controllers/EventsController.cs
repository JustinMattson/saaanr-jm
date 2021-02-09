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
  public class EventsController : ControllerBase
  {
    private readonly EventsService _es;
    public EventsController(EventsService es)
    {
      _es = es;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Event>> Get()
    {
      try
      {
        return Ok(_es.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("user")]
    [Authorize]
    public ActionResult<IEnumerable<Event>> GetEventsByUser()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_es.GetEventsByUser(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    // // get keep by ID may be required to update the counts (keeps and views)
    // // EventDetails?
    // [HttpGet("{id}")]
    // public ActionResult<Event> GetEventById(int id)
    // {
    //   try
    //   {
    //     string userId = "";
    //     var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
    //     if (claim != null)
    //     {
    //       userId = claim.Value;
    //     }
    //     return Ok(_es.GetEventById(id, userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   };
    // }

    // [HttpPost]
    // [Authorize]
    // public ActionResult<Event> Post([FromBody] Event newEvent)
    // {
    //   try
    //   {
    //     newEvent.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_es.Create(newEvent));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    // // EventToUpdate ktu
    // // This method should update keep.views onRouterEnter?
    // // This method should update keep.keeps when keep icon clicked
    // // This method should update isPrivate bool
    // [HttpPut("{id}")]
    // [Authorize]
    // public ActionResult<Event> Edit(int id, [FromBody] Event ktu)
    // {
    //   try
    //   {

    //     ktu.Id = id;
    //     string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    //     return Ok(_es.Edit(ktu, userId));
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
    //     return Ok(_es.Delete(id, userId));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
  }
}