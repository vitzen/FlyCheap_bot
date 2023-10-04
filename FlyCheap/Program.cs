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
        IReplyMarkup? mainMenu = null;
        await botClient.SendTextMessageAsync(tgId, "Choose Button:", replyMarkup: mainMenu);
        return;
    }

    if (user.InputState != InputState.Nothing)
    {
        if (user.InputState == InputState.DepartureСity)
        {
            //1. Попытаться спарсить текст в город (Город берем из листа)
            //1.1 если не получилось - ничего не меняя просим пользователя ввести еще раз, так как город не найден
            //2. Если найден -
            //2.1 изменить его state на arrival city
            //2.2 найти объект fly по user tg id и заполнить departure city
            //2.3 попросить пользователя ввести город назначения
        }

        if (user.InputState == InputState.ArrivalСity)
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
        создать и сохранить объект fly c заполненным полем user tg id
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

Метод который принимает обект fly и возвращает строку с найденными билетами
    Эту строку кинуть пользователю после успешного парсинга
Сделать GUID
Сделать обертку всем методам botclient.SendMessageTextAsync
    и callback методам