using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;


namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<VaultKeep> GetDTOvkByUser(string userId)
    {
      return _repo.GetDTOvkByUser(userId);
    }

    // Get User DTO Keeps by VaultId
    internal IEnumerable<VaultKeepViewModel> GetDTOKeepsByVaultId(int vaultId, string userId)
    {
      return _repo.GetDTOKeepsByVaultId(vaultId, userId);
    }

    // Get User VaultKeeps by vaultId
    internal IEnumerable<VaultKeep> GetDTOvByUser(int vaultId, string userId)
    {
      return _repo.GetDTOvByUser(vaultId, userId);
    }

    public VaultKeep Get(int id, string userId)
    {
      VaultKeep exists = _repo.GetById(id);
      if (exists == null)
      {
        throw new System.Exception("VaultKeep does not exist!");
      }
      if (exists.userId != userId)
      {
        throw new System.Exception("VaultKeep does not belong to you.");
      }
      return exists;
    }

    // public IEnumerable<VaultKeep> GetKeepsByVaultId(int id)
    // {
    //   return _repo.GetKeepsByVaultId(id);
    // }

    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      // TODO prevent duplicates from being created.
      return _repo.Create(newVaultKeep);
      // int id = _repo.Create(newVaultKeep);
      // newVaultKeep.id = id;
      // return newVaultKeep;
    }

    public string Delete(int id, string userId)
    {
      VaultKeep exists = Get(id, userId);
      _repo.Delete(id);
      return "VaultKeep successfully deleted.";
    }
  }
}