using System;
using System.Collections.Generic;
using Vacay.Models;
using Vacay.Repositories;

namespace Vacay.Services
{
  public class CruisesService
  {
    private readonly CruisesRepository _repo;
    public CruisesService(CruisesRepository repo)
    {
      _repo = repo;
    }

    internal List<Cruise> Get()
    {
      return _repo.Get();
    }

    internal Cruise GetById(int id)
    {
      Cruise found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id!");
      }
      return found;
    }
    internal Cruise Create(Cruise newCruise)
    {
      return _repo.Create(newCruise);
    }
    internal Cruise Edit(Cruise editedCruise, string userId)
    {
      Cruise oldCruise = GetById(editedCruise.Id);
      if (oldCruise.CreatorId != userId)
      {
        throw new Exception("You cannot edit this Cruise!");
      }
      editedCruise.Price = editedCruise.Price != 0 ? editedCruise.Price : oldCruise.Price;
      editedCruise.Destination = editedCruise.Destination != null ? editedCruise.Destination : oldCruise.Destination;
      editedCruise.Type = editedCruise.Type != null ? editedCruise.Type : oldCruise.Type;
      editedCruise.SailDate = editedCruise.SailDate != null ? editedCruise.SailDate : oldCruise.SailDate;
      return _repo.Edit(editedCruise);
    }
    internal void Remove(int id, string userId)
    {
      Cruise cruise = GetById(id);
      if (cruise.CreatorId != userId)
      {
        throw new Exception("You cannot Delete this Cruise!");
      }
      _repo.Remove(id);
    }
  }
}