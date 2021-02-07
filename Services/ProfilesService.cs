using System;
using System.Collections.Generic;
using SaaaNR.Models;
using SaaaNR.Repositories;

namespace SaaaNR.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;
    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Profile> Get()
    {
      return _repo.Get();
    }

    public IEnumerable<Profile> GetProfilesByUser(string userId)
    {
      return _repo.GetProfilesByUser(userId);
    }

    internal Profile GetProfileById(int id, string userId)
    {
      Profile foundProfile = _repo.GetProfileById(id);
      if (foundProfile == null)
      {
        throw new Exception("This profile does not exist.");
      }
      if (foundProfile.UserId != userId)
      {
        throw new Exception("This profile does not belong to you.");
      }
      return foundProfile;
    }

    public Profile Create(Profile newProfile)
    {
      return _repo.Create(newProfile);
    }

    // ProfileToUpdate (ptu)
    internal Profile Edit(Profile ptu)
    {
      Profile original = GetProfileById(ptu.Id, ptu.UserId);
      if (ptu == null || original.UserId != ptu.UserId)
      {
        throw new Exception("Invalid Id");
      }
      original.FirstName = ptu.FirstName == null ? original.FirstName : ptu.FirstName;
      original.LastName = ptu.LastName == null ? original.LastName : ptu.LastName;
      original.Email = ptu.Email == null ? original.Email : ptu.Email;
      original.Picture = ptu.Picture == null ? original.Picture : ptu.Picture;
      original.Phone = ptu.Phone == null ? original.Phone : ptu.Phone;
      // Author/userId is not to be changed. Ever.
      _repo.Edit(ptu);
      return original;
    }

    // Only user/author can delete
    // Probably easier to handle validation in Repository
    // Leaving similar to Keep.Delete for now...since it appears the keepr-testing-tool needs confimation.
    internal string Delete(int id, string userId)
    {
      Profile foundProfile = GetProfileById(id, userId);
      if (foundProfile.UserId != userId)
      {
        throw new Exception("This is not your Profile!");
      }
      if (_repo.Delete(id, userId))
      {
        return "successfully deleted.";
      }
      throw new Exception("Something unexpected happened.");
    }
  }
}