using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      // TINYINTs are boolean: False = 0, True = 1
      string sql = @"
      SELECT 
        k.*,vk.keeps 
      FROM keeps k
        LEFT JOIN (
          SELECT keepId,COUNT(*) as keeps
          FROM `saaanr`.`vaultkeeps` 
          GROUP BY keepId) vk ON k.id = vk.keepId
      WHERE isPrivate = 0;
      ";
      return _db.Query<Keep>(sql);
    }

    internal IEnumerable<Keep> GetKeepsByUser(string userId)
    {
      string sql = @"
        SELECT 
          * 
        FROM keeps k
          LEFT JOIN (
            SELECT keepId,COUNT(*) as keeps
            FROM `saaanr`.`vaultkeeps` 
            GROUP BY keepId) vk ON k.id = vk.keepId
        WHERE userId = @userId;";
      return _db.Query<Keep>(sql, new { userId });
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = @id)
      WHERE id = @id;
      
      SELECT * FROM keeps WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal Keep Create(Keep KeepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, userId, img, isPrivate, views, shares, keeps)
      VALUES
      (@Name, @Description, @UserId, @img, @isPrivate, @Views, @shares, @Keeps);

      SELECT LAST_INSERT_ID();";
      KeepData.Id = _db.ExecuteScalar<int>(sql, KeepData);
      return KeepData;
    }

    internal Keep Edit(Keep keepToUpdate)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        userId = @UserId,
        img = @img,
        isPrivate = @isPrivate,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
      WHERE id = @Id;
      
      SELECT * FROM keeps WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Keep>(sql, keepToUpdate);
    }

    internal bool Delete(int id, string userId)
    {
      // User/Author validated in service
      // Just need to make sure the Keep != private.
      string sql = @"
      DELETE FROM keeps WHERE id = @id;
      ";// AND isPrivate = 1;";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }
  }
}