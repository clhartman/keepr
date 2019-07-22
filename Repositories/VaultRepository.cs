using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }
    public IEnumerable<Vault> GetByUserId(string userId)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE userId = @userId", new { userId });
    }

    public Vault GetById(int id)
    {
      string query = "SELECT * FROM vaults WHERE id = @id";
      Vault vault = _db.QueryFirstOrDefault<Vault>(query, new { id });
      if (vault == null) throw new Exception("Invalidi Id");
      return vault;
    }

    public Vault Create(Vault value)
    {
      string query = @"
        INSERT INTO vaults (name, description, userId)
            VALUE (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(query, value);
      value.Id = id;
      return value;
    }

    public Vault Update(Vault value)
    {
      string query = @"
      UPDATE vaults
      SET
        name = @Name,
        description = @Description
      WHERE id = @Id;
      SELECT * FROM vaults WHERE id = @Id;
      ";
      return _db.QueryFirstOrDefault<Vault>(query, value);
    }

    public string Delete(int id)
    {
      string query = "DELETE FROM vaults WHERE id = @id";
      int changedRows = _db.Execute(query, new { id });
      if (changedRows < 1) throw new Exception("Invalid Id");
      return "Successfully Deleted Vault";
    }
  }
}