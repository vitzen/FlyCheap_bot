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

    public Fly(long tgId)
    {
        throw new NotImplementedException();
    }
}

//1. Попытаться спарсить текст в город (Город берем из листа)
//1.1 если не получилось - ничего не меняя просим пользователя ввести еще раз, так как город не найден
//2. Если найден -
//2.1 изменить его state на arrival city
//2.2 найти объект fly по user tg id и заполнить departure city
//2.3 попросить пользователя ввести город назначения