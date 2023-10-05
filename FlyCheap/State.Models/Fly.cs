using System.ComponentModel.DataAnnotations;

namespace FlyCheap.State.Models;

public class Fly
{
    public Guid Id { get; set; }
    public IEnumerable<string> DepartureСity { get; set; }
    public IEnumerable<string> ArrivalСity { get; set; }
    public IEnumerable<string> DepartureDate { get; set; }
    public int PassengersNumber { get; set; } = 1; //Количество пассажиров
    public int TransfersNumber { get; set; } = 0; //Количество пересадок
    public long UserTgId { get; set; }
    public string resultTickets;

    public Fly(long tgId)
    {
        throw new NotImplementedException();
    }
}