using Telegram.Bot.Types.ReplyMarkups;

namespace FlyCheap;

public static class Keyboard
{
    static ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    {
        new KeyboardButton[] { "flight search" },
        new KeyboardButton[] { "favorites️" },
    })
    {
        ResizeKeyboard = true
    }; 
}