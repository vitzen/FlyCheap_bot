using FlyCheap;
using FlyCheap.State.Models;
using FlyCheap.UI;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

#region

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

#endregion

//Метод для обработки обновлений бота
async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleCommandMessage(botClient, update.Message);
        return;
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallbackQuery(botClient, update.CallbackQuery);
        return;
    }
}

//Метод ожидающий ввода пользователем команды c inline клавиатуры
async Task HandleCommandMessage(ITelegramBotClient botClient, Message message)
{
    var tgId = message.From.Id;
    var user = UserUtils.GetOrCreate(tgId);
    var text = message.Text.ToLower();

    if (text == "/start")
    {
        await botClient.SendTextMessageAsync(tgId, "Choose Button:", replyMarkup: MainMenu.GetMainMenu());
        return;
    }

    if (user.InputState != InputState.Nothing)
    {
        if (user.InputState == InputState.DepartureСity)
        {
            if (message.Text != null && message.Text == "moscow")
            {
                user.InputState = InputState.ArrivalСity;
                var fly =
                    await botClient.SendTextMessageAsync(tgId, "вы ввели москва, теперь введите город назначения:");

                return;
            }
            else
            {
                user.InputState = InputState.DepartureСity;
                await botClient.SendTextMessageAsync(tgId, "Город отправления не найден - повторите ввод:");
            }
        }

        if (user.InputState == InputState.ArrivalСity)
        {
            if (message.Text != null && message.Text == "voronez")
            {
                await botClient.SendTextMessageAsync(tgId, "вы ввели воронеж, теперь введите дату отправления:");
                user.InputState = InputState.DepartureDate;
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(tgId, "Город прибытия не найден - повторите ввод:");
                user.InputState = InputState.ArrivalСity;
            }
        }

        if (user.InputState == InputState.DepartureDate)
        {
            // Все аналогично только следующий шаг  
        }

        //Дату спарсить до datetime
        DateTime.Parse("Спарсим");
        //DateTime.Now.ToString (взять только дату)
    }

//дефолтный ответ бота в случае неправильного ввода команды пользователем
    await botClient.SendTextMessageAsync(tgId, $"To start working with the bot, send the command /start \n");
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Метод обрабатывающий нажатие определенной кнопки inline клавиатуры
async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    var tgId = callbackQuery.From.Id;
    var user = UserUtils.GetOrCreate(tgId);

    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

    //Поиск авиабилетов ----->>>>>>>>>>>>>>>>>>>>>>>>
    if (callbackQuery.Data.StartsWith("searchFlight"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Enter the city of departure"
        );

        user.InputState = InputState.DepartureСity;
        var fly = new Fly(tgId);
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

// Метод который принимает обект fly и возвращает строку с найденными билетами
//     Эту строку кинуть пользователю после успешного парсинга
// Сделать GUID
// Сделать обертку всем методам botclient.SendMessageTextAsync
//     и callback методам