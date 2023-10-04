using Telegram.Bot.Types.ReplyMarkups;

namespace FlyCheap.UI;

public static class MainMenu
{
    static InlineKeyboardMarkup mainMenu = new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Search Flight", "searchFlight"),
            InlineKeyboardButton.WithCallbackData("My Flights", "myFlights"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Restart", "restartBot"),
        },
    });
}