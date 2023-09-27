using System.ComponentModel.DataAnnotations;

namespace FlyCheap.State.Models;

public class UserRoles
{
    [Key]
    public long TgId { get; set; }
    public string Role { get; set; }
    public string TgUsername { get; set; }
    public long TgChatId { get; set; }
    public int StageReg { get; set; }
}