using System;

namespace Vacay.Interfaces
{
  public interface IVacation
  {
    public int Id { get; set; }
    public float Price { get; set; }
    public string Destination { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}