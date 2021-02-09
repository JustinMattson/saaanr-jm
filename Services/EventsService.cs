using System;
using System.Collections.Generic;
using SaaaNR.Models;
using SaaaNR.Repositories;

namespace SaaaNR.Services
{
  public class EventsService
  {
    private readonly EventsRepository _repo;
    public EventsService(EventsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Event> Get()
    {
      return _repo.Get();
    }

    internal IEnumerable<Event> GetEventsByUser(string userId)
    {
      return _repo.GetEventsByUser(userId);
    }

    // internal Event GetEventById(int id, string userId)
    // {
    //   Event foundEvent = _repo.GetEventById(id);
    //   if (foundEvent == null)
    //   {
    //     throw new Exception("Invalid Id");
    //   }
    //   // NOTE private keeps are only visible to author/user
    //   if (foundEvent.IsPrivate == true && foundEvent.UserId != userId)
    //   {
    //     throw new Exception("Some Events are PRIVATE!");
    //   }
    //   return foundEvent;
    // }

    // public Event Create(Event newEvent)
    // {
    //   return _repo.Create(newEvent);
    // }

    // EventToUpdate (ktu) can be updated by clicking the Event button
    // Count keeps++ when added to Vault
    // Count keeps-- when removed from Vault
    // Count view++ on router EventsDetails
    // internal Event Edit(Event ktu, string userId)
    // {
    //   Event original = GetEventById(ktu.Id, userId);
    //   if (ktu == null)
    //   {
    //     throw new Exception("Invalid Id");
    //   }
    //   if (ktu.IsPrivate && ktu.UserId != userId)
    //   {
    //     throw new Exception("You cannot edit others' private posts!");
    //   }
    //   if (ktu.UserId != userId)
    //   {
    //     throw new Exception("You cannot edit others' private posts!");
    //   }
    //   // original.Name = ktu.Name == null ? original.Name : ktu.Name;
    //   // original.Description = ktu.Name == null ? original.Description : ktu.Description;
    //   // Author/userId is not to be changed. Ever.
    //   // original.Img = ktu.Img == null ? original.Img : ktu.Img;
    //   original.IsPrivate = ktu.IsPrivate == original.IsPrivate ? original.IsPrivate : ktu.IsPrivate;
    //   original.Views = ktu.Views != 0 ? ktu.Views : original.Views;
    //   // original.Shares = ktu.Shares != 0 ? ktu.Shares : original.Shares;
    //   original.Events = ktu.Events != 0 ? ktu.Events : original.Events;
    //   _repo.Edit(ktu);
    //   return original;
    // }

    // Only user/author can delete, and only if isPrivate == true
    // internal string Delete(int id, string userId)
    // {
    //   Event foundEvent = GetEventById(id, userId);
    //   if (foundEvent.UserId != userId)
    //   {
    //     throw new Exception("This is not your Event to delete!");
    //   }
    //   if (_repo.Delete(id, userId))
    //   {
    //     return "Event successfully deleted.";
    //   }
    //   throw new Exception("Public Events cannot be deleted! Says who?");
    // }
  }
}