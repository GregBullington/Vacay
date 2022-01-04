using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Vacay.Models;

namespace Vacay.Repositories
{
  public class CruisesRepository
  {
    private readonly IDbConnection _db;

    public CruisesRepository(IDbConnection db)
    {
      _db = db;
    }


    internal List<Cruise> Get()
    {
      string sql = "SELECT * FROM cruise;";
      return _db.Query<Cruise>(sql).ToList();
    }

    internal Cruise GetById(int id)
    {
      string sql = @"SELECT * FROM cruise WHERE id = @id;";
      return _db.QueryFirstOrDefault<Cruise>(sql, new { id });
    }

    internal Cruise Create(Cruise newCruise)
    {
      string sql = @"INSERT INTO cruise
      (price, destination, type, sailDate, creatorId)
      VALUE(@Price, @Destination, @Type, @SailDate, @CreatorId)
      ;SELECT LAST_INSERT_ID();";

      int id = _db.ExecuteScalar<int>(sql, newCruise);
      newCruise.Id = id;
      return newCruise;
    }

    internal Cruise Edit(Cruise editedCruise)
    {
      string sql = @"UPDATE cruise
      SET price = @Price, destination = @Destination, type = @Type, sailDate = @SailDate, creatorId = @CreatorId
      WHERE id = @Id;";
      int rows = _db.Execute(sql, editedCruise);
      if (rows <= 0)
      {
        throw new Exception("REPO Cruise Edit was not successful!");
      }
      return editedCruise;
    }
    internal void Remove(int id)
    {
      string sql =
      @"DELETE FROM cruise WHERE id = @Id;";
      int rows = _db.Execute(sql, new { id });
      if (rows <= 0)
      {
        throw new Exception("Invalid Id!");
      }
    }
  }
}