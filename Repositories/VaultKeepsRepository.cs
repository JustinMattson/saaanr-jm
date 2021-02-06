using System.Data;
using System.Collections.Generic;
using Dapper;
using Keepr.Models;
using System;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<VaultKeep> GetDTOvkByUser(string userId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE userId = @userId;";
      return _db.Query<VaultKeep>(sql, new { userId });
    }

    // Get User DTO Keeps by vaultId
    internal IEnumerable<VaultKeepViewModel> GetDTOKeepsByVaultId(int vaultId, string userId)
    {
      // SELECT 
      // k.*,
      // vk.id as vaultKeepId
      // FROM vaultkeeps vk
      // INNER JOIN keeps k ON k.id = vk.keepId 
      // WHERE (vaultId = @vaultId AND vk.userId = @userId)

      string sql = @"
        SELECT
              vk.id as vaultKeepId,
              vk.vaultId as vaultId,
              k.id,
              k.userId as userId,
              k.name as name,
              k.description as description,
              k.img as img,
              k.isPrivate as isPrivate,
              k.views as views,
              k.shares as shares,
              kc.keeps as keeps
        FROM vaultkeeps vk
              LEFT JOIN (
                    SELECT 
                    keepId,COUNT(*) as keeps
                    FROM `saaanr`.`vaultkeeps` 
                    GROUP BY keepId
              ) kc ON vk.keepId = kc.keepId
              INNER JOIN keeps k ON k.id = vk.keepId
        WHERE (vk.vaultID = @vaultId AND vk.userId = @userId);
      ";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId, userId });
    }

    // Get User VaultKeeps by vaultId
    internal IEnumerable<VaultKeep> GetDTOvByUser(int id, string userId)
    {
      string sql = @"
        SELECT * FROM vaultkeeps WHERE vaultId = @id && userId = @userId;
      ";
      return _db.Query<VaultKeep>(sql, new { id, userId });
    }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps where id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }


    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
      INSERT INTO vaultkeeps
        (vaultId, keepId, userId)
      VALUES
        (@vaultId, @KeepId, @UserId);
      
      UPDATE keeps
      SET keeps = (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = @keepId)
      WHERE id = @keepId;

      SELECT LAST_INSERT_ID();";
      newVaultKeep.id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      return newVaultKeep;
    }

    internal void Delete(int id)
    {
      // FIXME when this executes, it updates the database, but the 
      // home view doesn't reflect the keep being removed from a vault.
      // duplicate the create with a return type?
      string sql = @"
      DELETE FROM VaultKeeps WHERE id = @Id;

      UPDATE keeps
      SET keeps = 
        (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = 
          (SELECT keepId FROM `saaanr`.`vaultkeeps` WHERE id = @Id))
      WHERE id = (SELECT keepId FROM `saaanr`.`vaultkeeps` WHERE id = @Id);
      ";
      _db.Execute(sql, new { id });
      // REVIEW this should probably return something!?
    }
  }
}