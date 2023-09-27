namespace FlyCheap;

public static class Configuration
{
    private static readonly string token = "5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE";
    private static readonly string connectionString = "Host=localhost;Username=postgres;Password=root;Database=Maxima";

    public static string Token => token;

    public static string ConnectionString => connectionString;
}