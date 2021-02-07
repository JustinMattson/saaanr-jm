using System;
using System.Collections.Generic;
using SaaaNR.Models;
using SaaaNR.Repositories;

namespace SaaaNR.Services
{
  public class ContactsService
  {
    private readonly ContactsRepository _repo;
    public ContactsService(ContactsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Contact> Get()
    {
      return _repo.Get();
    }

    internal IEnumerable<Contact> GetContactsByUser(string userId)
    {
      return _repo.GetContactsByUser(userId);
    }

    // internal Contact GetContactById(int id, string userId)
    // {
    //   Contact foundContact = _repo.GetContactById(id);
    //   if (foundContact == null)
    //   {
    //     throw new Exception("Invalid Id");
    //   }
    //   // NOTE private keeps are only visible to author/user
    //   if (foundContact.IsPrivate == true && foundContact.UserId != userId)
    //   {
    //     throw new Exception("Some Contacts are PRIVATE!");
    //   }
    //   return foundContact;
    // }

    // public Contact Create(Contact newContact)
    // {
    //   return _repo.Create(newContact);
    // }

    // ContactToUpdate (ktu) can be updated by clicking the Contact button
    // Count keeps++ when added to Vault
    // Count keeps-- when removed from Vault
    // Count view++ on router ContactsDetails
    // internal Contact Edit(Contact ktu, string userId)
    // {
    //   Contact original = GetContactById(ktu.Id, userId);
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
    //   original.Contacts = ktu.Contacts != 0 ? ktu.Contacts : original.Contacts;
    //   _repo.Edit(ktu);
    //   return original;
    // }

    // Only user/author can delete, and only if isPrivate == true
    // internal string Delete(int id, string userId)
    // {
    //   Contact foundContact = GetContactById(id, userId);
    //   if (foundContact.UserId != userId)
    //   {
    //     throw new Exception("This is not your Contact to delete!");
    //   }
    //   if (_repo.Delete(id, userId))
    //   {
    //     return "Contact successfully deleted.";
    //   }
    //   throw new Exception("Public Contacts cannot be deleted! Says who?");
    // }
  }
}