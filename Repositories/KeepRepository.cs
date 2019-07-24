using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }

    public IEnumerable<Keep> GetAllPublic()
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE isPrivate = 0");
    }

    public IEnumerable<Keep> GetByUserId(string userId)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE userId = @userId", new { userId });
    }

    public Keep GetById(int id)
    {
      string query = "SELECT * FROM keeps WHERE id = @id";
      Keep keep = _db.QueryFirstOrDefault<Keep>(query, new { id });
      if (keep == null) throw new Exception("Invalid ID");
      return keep;
    }

    public Keep Create(Keep value)
    {
      string query = @"
      INSERT INTO keeps (name, description, userId, img, isPrivate)
        VALUES (@Name, @Description, @UserId, @Img, @IsPrivate);
        SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(query, value);
      value.Id = id;
      return value;
    }

    public Keep Update(Keep value)
    {
      string query = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps,
      WHERE id = @Id;
      SELECT * FROM keeps WHERE id = @Id;
      ";
      return _db.QueryFirstOrDefault<Keep>(query, value);
    }

    public string Delete(int id)
    {
      string query = "DELETE FROM keeps WHERE id = @Id";
      int changedRows = _db.Execute(query, new { id });
      if (changedRows < 1) throw new Exception("Invalid Id");
      return "Successfully deleted keep";
    }
  }
}