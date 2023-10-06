using System.ComponentModel.DataAnnotations;

namespace FlyCheap.State.Models;

public class Fly
{
    public Guid Id { get; set; }
    public string DepartureСity { get; set; }
    public string ArrivalСity { get; set; }
    public DateTime DepartureDate { get; set; }
    public int PassengersNumber { get; set; } = 1; //Количество пассажиров
    public int TransfersNumber { get; set; } = 0; //Количество пересадок
    public long UserTgId { get; set; }
    public string resultTickets;

    public Fly(long tgId)
    {
        throw new NotImplementedException();
    }

    public Fly(Guid id, string departureСity, string arrivalСity, DateTime departureDate, int passengersNumber, int transfersNumber, long userTgId)
    {
        Id = id;
        DepartureСity = departureСity;
        ArrivalСity = arrivalСity;
        DepartureDate = departureDate;
        PassengersNumber = passengersNumber;
        TransfersNumber = transfersNumber;
        UserTgId = userTgId;
    }
}