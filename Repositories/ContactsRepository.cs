using System.Collections.Generic;
using System.Data;
using SaaaNR.Models;
using Dapper;

namespace SaaaNR.Repositories
{
  public class ContactsRepository
  {
    private readonly IDbConnection _db;

    public ContactsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Contact> Get()
    {
      string sql = "SELECT * FROM contacts;";
      return _db.Query<Contact>(sql);
    }

    internal IEnumerable<Contact> GetContactsByUser(string userId)
    {
      string sql = "SELECT * FROM Contacts;";
      return _db.Query<Contact>(sql, new { userId });
    }

    // internal Contact GetContactById(int id)
    // {
    //   string sql = @"
    //   UPDATE keeps
    //   SET keeps = (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = @id)
    //   WHERE id = @id;

    //   SELECT * FROM keeps WHERE id = @id;
    //   ";
    //   return _db.QueryFirstOrDefault<Contact>(sql, new { id });
    // }

    // internal Contact Create(Contact ContactData)
    // {
    //   string sql = @"
    //   INSERT INTO keeps
    //   (name, description, userId, img, isPrivate, views, shares, keeps)
    //   VALUES
    //   (@Name, @Description, @UserId, @img, @isPrivate, @Views, @shares, @Contacts);

    //   SELECT LAST_INSERT_ID();";
    //   ContactData.Id = _db.ExecuteScalar<int>(sql, ContactData);
    //   return ContactData;
    // }

    // internal Contact Edit(Contact keepToUpdate)
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
    //     keeps = @Contacts
    //   WHERE id = @Id;

    //   SELECT * FROM keeps WHERE id = @Id;";
    //   return _db.QueryFirstOrDefault<Contact>(sql, keepToUpdate);
    // }

    // internal bool Delete(int id, string userId)
    // {
    //   // User/Author validated in service
    //   // Just need to make sure the Contact != private.
    //   string sql = @"
    //   DELETE FROM keeps WHERE id = @id;
    //   ";// AND isPrivate = 1;";
    //   int affectedRows = _db.Execute(sql, new { id, userId });
    //   return affectedRows == 1;
    // }
  }
}