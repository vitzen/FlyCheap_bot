using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE");

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};

// Тест коннекта с ботом TG / Прослушка работы бота
botClient.StartReceiving(
    HandleUpdatesAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Начал прослушку @{me.Username}");
Console.ReadLine();

cts.Cancel();

//Метод для обработки обновлений бота
async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleMessage(botClient, update.Message);
        return;
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallbackQuery(botClient, update.CallbackQuery);
        return;
    }
}

//Метод ожидающий ввода пользователем команды
async Task HandleMessage(ITelegramBotClient botClient, Message message)
{
    if (message.Text == "/start")
    {
        ReplyKeyboardMarkup keyboard = new(new[]
        {
            new KeyboardButton[] { "Search Flight", "My Flights" },
            new KeyboardButton[] { "Restart", "Test" }
        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите действие:", replyMarkup: keyboard);
        return;
    }

    if (message.Text == "/inline")
    {
        InlineKeyboardMarkup keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Search Flight", "searchFlight"),
                InlineKeyboardButton.WithCallbackData("My Flights", "myFlights"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Restart", "restartBot"),
                InlineKeyboardButton.WithCallbackData("Test", "test"),
            },
        });
        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose inline:", replyMarkup: keyboard);
        return;
    }

    //дефолтный ответ бота в случае неправильного ввода команды пользователем
    //await botClient.SendTextMessageAsync(message.Chat.Id, $"You said:\n{message.Text}");
    await botClient.SendTextMessageAsync(message.Chat.Id, $"Для начала работы с ботом отправьте команду /start \n");
}

async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    if (callbackQuery.Data.StartsWith("searchFlight"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы выбрали Search Flight"
        );
        return;
    }

    if (callbackQuery.Data.StartsWith("myFlights"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы выбрали My Flights"
        );
        return;
    }

    if (callbackQuery.Data.StartsWith("restartBot"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы выбрали Restart"
        );
        return;
    }

    if (callbackQuery.Data.StartsWith("test"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы выбрали Test"
        );
        return;
    }

    await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"You choose with data: {callbackQuery.Data}"
    );
    return;
}

//Метод для обработки ошибок (бота или API телеграмма)
Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Ошибка телеграм API:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
        _ => exception.ToString()
    };
    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}