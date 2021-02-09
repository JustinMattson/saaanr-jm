using System.Collections.Generic;
using System.Data;
using SaaaNR.Models;
using Dapper;

namespace SaaaNR.Repositories
{
  public class EventsRepository
  {
    private readonly IDbConnection _db;

    public EventsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Event> Get()
    {
      string sql = "SELECT * FROM events;";
      return _db.Query<Event>(sql);
    }

    // internal Event GetEventById(int id)
    // {
    //   string sql = @"
    //   UPDATE keeps
    //   SET keeps = (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = @id)
    //   WHERE id = @id;

    //   SELECT * FROM keeps WHERE id = @id;
    //   ";
    //   return _db.QueryFirstOrDefault<Event>(sql, new { id });
    // }

    // internal Event Create(Event EventData)
    // {
    //   string sql = @"
    //   INSERT INTO keeps
    //   (name, description, userId, img, isPrivate, views, shares, keeps)
    //   VALUES
    //   (@Name, @Description, @UserId, @img, @isPrivate, @Views, @shares, @Events);

    //   SELECT LAST_INSERT_ID();";
    //   EventData.Id = _db.ExecuteScalar<int>(sql, EventData);
    //   return EventData;
    // }

    // internal Event Edit(Event keepToUpdate)
    // {
    //   string sql = @"
    //   UPDATE keeps
    //   SET
    //     name = @Name,
    //     description = @Description,
    //     userId = @UserId,
    //     img = @img,
    //     isPrivate = @isPrivate,
    //     views = @Views,
    //     shares = @Shares,
    //     keeps = @Events
    //   WHERE id = @Id;

    //   SELECT * FROM keeps WHERE id = @Id;";
    //   return _db.QueryFirstOrDefault<Event>(sql, keepToUpdate);
    // }

    // internal bool Delete(int id, string userId)
    // {
    //   // User/Author validated in service
    //   // Just need to make sure the Event != private.
    //   string sql = @"
    //   DELETE FROM keeps WHERE id = @id;
    //   ";// AND isPrivate = 1;";
    //   int affectedRows = _db.Execute(sql, new { id, userId });
    //   return affectedRows == 1;
    // }
  }
}