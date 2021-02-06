using System.Collections.Generic;
using System.Data;
using SaaaNR.Models;
using Dapper;

namespace SaaaNR.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Profile> GetProfilesByUser(string userId)
    {
      string sql = "SELECT * FROM profiles WHERE userId = @userId;";
      return _db.Query<Profile>(sql, new { userId });
    }

    // internal IEnumerable<Profile> GetProfilesByUserId(string userId)
    // {
    //   string sql = "SELECT * FROM Profiles WHERE userId = @userId;";
    //   return _db.Query<Profile>(sql, new { userId });
    // }

    internal Profile GetProfileById(int id)
    {
      string sql = "SELECT * FROM profiles WHERE id = @id;";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal Profile Create(Profile ProfileData)
    {
      string sql = @"
      INSERT INTO profiles
      (firstname, lastname, email, picture, phone, userId)
      VALUES
      (@FirstName, @LastName, @Email, @Picture, @Phone, @UserId);
      SELECT LAST_INSERT_ID();";
      ProfileData.Id = _db.ExecuteScalar<int>(sql, ProfileData);
      return ProfileData;
    }

    internal Profile Edit(Profile profileToUpdate)
    {
      string sql = @"
      UPDATE profiles
      SET
        firstname = @FirstName,
        lastname = @LastName,
        email = @Email,
        picture = @Picture,
        phone = @Phone,
        userId = @UserId
      WHERE id = @Id;
      SELECT * FROM profiles WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Profile>(sql, profileToUpdate);
    }

    internal bool Delete(int id, string userId)
    {
      // User/Author validated in service
      // Just need to make sure the Profile != private.
      string sql = "DELETE FROM profiles WHERE id = @id AND userId = @UserId;";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }
  }
}