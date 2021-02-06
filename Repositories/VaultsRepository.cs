using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> GetVaultsByUser(string userId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @userId;";
      return _db.Query<Vault>(sql, new { userId });
    }

    // internal IEnumerable<Vault> GetVaultsByUserId(string userId)
    // {
    //   string sql = "SELECT * FROM Vaults WHERE userId = @userId;";
    //   return _db.Query<Vault>(sql, new { userId });
    // }

    internal Vault GetVaultById(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @id;";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }

    internal Vault Create(Vault VaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, userId)
      VALUES
      (@Name, @Description, @UserId);
      SELECT LAST_INSERT_ID();";
      VaultData.Id = _db.ExecuteScalar<int>(sql, VaultData);
      return VaultData;
    }

    internal Vault Edit(Vault vaultToUpdate)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        description = @Description,
        userId = @UserId
      WHERE id = @Id;
      SELECT * FROM vaults WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Vault>(sql, vaultToUpdate);
    }

    internal bool Delete(int id, string userId)
    {
      // User/Author validated in service
      // Just need to make sure the Vault != private.
      string sql = "DELETE FROM vaults WHERE id = @id AND userId = @UserId;";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }
  }
}