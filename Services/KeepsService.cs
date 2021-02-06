using System;
using System.Collections.Generic;
using SaaaNR.Models;
using SaaaNR.Repositories;

namespace SaaaNR.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }

    internal IEnumerable<Keep> GetKeepsByUser(string userId)
    {
      return _repo.GetKeepsByUser(userId);
    }

    internal Keep GetKeepById(int id, string userId)
    {
      Keep foundKeep = _repo.GetKeepById(id);
      if (foundKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      // NOTE private keeps are only visible to author/user
      if (foundKeep.IsPrivate == true && foundKeep.UserId != userId)
      {
        throw new Exception("Some Keeps are PRIVATE!");
      }
      return foundKeep;
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    // KeepToUpdate (ktu) can be updated by clicking the Keep button
    // Count keeps++ when added to Vault
    // Count keeps-- when removed from Vault
    // Count view++ on router KeepsDetails
    internal Keep Edit(Keep ktu, string userId)
    {
      Keep original = GetKeepById(ktu.Id, userId);
      if (ktu == null)
      {
        throw new Exception("Invalid Id");
      }
      if (ktu.IsPrivate && ktu.UserId != userId)
      {
        throw new Exception("You cannot edit others' private posts!");
      }
      if (ktu.UserId != userId)
      {
        throw new Exception("You cannot edit others' private posts!");
      }
      // original.Name = ktu.Name == null ? original.Name : ktu.Name;
      // original.Description = ktu.Name == null ? original.Description : ktu.Description;
      // Author/userId is not to be changed. Ever.
      // original.Img = ktu.Img == null ? original.Img : ktu.Img;
      original.IsPrivate = ktu.IsPrivate == original.IsPrivate ? original.IsPrivate : ktu.IsPrivate;
      original.Views = ktu.Views != 0 ? ktu.Views : original.Views;
      // original.Shares = ktu.Shares != 0 ? ktu.Shares : original.Shares;
      original.Keeps = ktu.Keeps != 0 ? ktu.Keeps : original.Keeps;
      _repo.Edit(ktu);
      return original;
    }

    // Only user/author can delete, and only if isPrivate == true
    internal string Delete(int id, string userId)
    {
      Keep foundKeep = GetKeepById(id, userId);
      if (foundKeep.UserId != userId)
      {
        throw new Exception("This is not your Keep to delete!");
      }
      if (_repo.Delete(id, userId))
      {
        return "Keep successfully deleted.";
      }
      throw new Exception("Public Keeps cannot be deleted! Says who?");
    }
  }
}