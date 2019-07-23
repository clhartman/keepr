using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }
    public VaultKeep Create(VaultKeep value)
    {
      string query = @"
      INSERT INTO vaultkeeps (vaultId, keepId, userId)
        VALUES (@VaultId, @KeepId, @UserId);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(query, value);
      value.Id = id;
      return value;
    }

    public IEnumerable<Keep> GetByVaultId(int vaultId, string userId)
    {
      return _db.Query<Keep>(@"
      SELECT * FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId
        WHERE(vaultId = @VaultId AND vk.userId = @UserId);
      ",
      new { vaultId, userId });
    }

    public string Delete(VaultKeep value)
    {
      string query = "DELETE FROM vaultkeeps WHERE (keepid = @KeepId AND vaultId = @VaultId AND userId = @UserId)";
      int changedRows = _db.Execute(query, value);
      if (changedRows < 1) throw new Exception("Invalid Id");
      return "VaultKeep Deleted";
    }
  }
}