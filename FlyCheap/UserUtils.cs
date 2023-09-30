using FlyCheap.Db.Models;
using FlyCheap.State.Models;

namespace FlyCheap;

public static class UserUtils
{
    public static User GetOrCreate(long tgId)
    {
        var user = Context.Users.FirstOrDefault(x => x.TgId == tgId);
        if (user != null)
        {
            return user;
        }
        var newUser = Context.Users.
    }
}