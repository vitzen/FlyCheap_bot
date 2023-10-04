using System.ComponentModel.DataAnnotations;

namespace FlyCheap.State.Models;

public class User
{
    [Key]
    public long TgId { get; set; }
    public Role Role { get; set; }
    public InputState InputState { get; set; }
    public string TgUsername { get; set; }
    public bool IsRegistered { get; set; }
    
}

public enum InputState
{
    Nothing,
    DepartureСity,
    ArrivalСity,
    DepartureDate,
}


