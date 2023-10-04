using FlyCheap.State.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyCheap.Db.Models;

public static class Context // : DbContext
{
    public static List<User> Users = new List<User>();
    public static List<Fly> Flyes = new List<Fly>();
}