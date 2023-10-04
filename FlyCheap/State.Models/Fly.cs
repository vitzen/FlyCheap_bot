using System.ComponentModel.DataAnnotations;

namespace FlyCheap.State.Models;

public class Fly
{
    [Key] public Guid Id { get; set; }
    public string DepartureСity { get; set; }
    public string ArrivalСity { get; set; }
    public DateTime DepartureDate { get; set; }
    public int PassengersNumber { get; set; } = 1; //Количество пассажиров
    public int TransfersNumber { get; set; } = 0; //Количество пересадок
    public long UserTgId { get; set; }
    public string resultTickets;
}