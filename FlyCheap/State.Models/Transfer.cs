using System.ComponentModel.DataAnnotations;

namespace FlyCheap.State.Models;

public class Transfer
{
    [Key]
    public string TransferId { get; set; }
    public string DepartureСity { get; set; }
    public string ArrivalСity { get; set; }
    public DateTime DepartureDate { get; set; }
    public byte PassengersNumber { get; set; } //Количество пассажиров
    public byte TransfersNumber { get; set; } //Количество пересадок
    public UserRoles UserRoles { get; set; }
}