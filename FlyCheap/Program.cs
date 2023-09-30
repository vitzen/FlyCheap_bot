using FlyCheap;
using FlyCheap.State.Models;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

//Словарь, хранящий Id чата и класс, где описано состояние SearchState
//var userSearchState = new Dictionary<long, UserRoles>();

var botClient = new TelegramBotClient(Configuration.Token);

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};

// Тест коннекта с ботом TG / Прослушка работы бота
botClient.StartReceiving(
    HandleUpdatesAsync,
    Exceptions.HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
await Task.Delay(Int32.MaxValue);

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

//Метод ожидающий ввода пользователем команды c inline клавиатуры
async Task HandleMessage(ITelegramBotClient botClient, Message message)
{
    var tgId = message.From.Id;
    var user = UserUtils.GetOrCreate(tgId);
    
    if (message.Text.ToLower() == "/start")
    {
        #region [Main menu]

        InlineKeyboardMarkup mainMenu = new(new[]
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

        #endregion

        await botClient.SendTextMessageAsync(message.Chat.Id, "Choose Button:", replyMarkup: mainMenu);
        return;
    }

    //дефолтный ответ бота в случае неправильного ввода команды пользователем
    await botClient.SendTextMessageAsync(message.Chat.Id, $"To start working with the bot, send the command /start \n");
}

//Метод обрабатывающий нажатие определенной кнопки inline клавиатуры
async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

    //Поиск авиабилетов ----->>>>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("searchFlight"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Enter the city of departure"
        );
        return;
    }

    //Мои рейсы/избранное ----->>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("myFlights"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"You choose My Flights"
        );
        return;
    }

    //Перезапуск бота ----->>>>>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("restartBot"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"You choose Restart"
        );
        return;
    }

    await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"You choose with data: {callbackQuery.Data}"
    );
    return;
}