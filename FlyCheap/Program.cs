using FlyCheap;
using FlyCheap.Collections;
using FlyCheap.State.Models;
using FlyCheap.UI;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;

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
        ////////////////////////////////////////////////////////////////////////////////////// ПАРСИМ ГОРОД ОТПРАВЛЕНИЯ
        if (user.InputState == InputState.DepartureСity)
        {
            var cityFromMessage = message.Text.ToLower();
            var selectedCityFromList = CitiesCollection.cities
                .Where(x => x.Contains(cityFromMessage))
                .First()
                .ToString();
            
            if (selectedCityFromList != null)
            {
                var flight = FlightsList.flights
                    .Where(x => x.UserTgId == tgId)
                    .Where(x => x.DepartureСity == null)
                    .FirstOrDefault();

                Guid guidId = Guid.NewGuid();

                flight.DepartureСity = cityFromMessage;
                flight.Id = guidId;

                await botClient.SendTextMessageAsync(tgId, $"ваш город отправления {cityFromMessage}, теперь введите город назначения:");
                user.InputState = InputState.ArrivalСity;
                return;
            }
            else
            {
                user.InputState = InputState.DepartureСity;
                await botClient.SendTextMessageAsync(tgId, "Город отправления не найден - повторите ввод:");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////// ПАРСИМ ГОРОД ПРИБЫТИЯ
        if (user.InputState == InputState.ArrivalСity)
        {
            var cityFromMessage = message.Text.ToLower();
            var selectedCityFromList = CitiesCollection.cities
                .Where(x => x.Contains(cityFromMessage))
                .First()
                .ToString();
                
            if (selectedCityFromList != null)
            {
                var flight = FlightsList.flights
                    .Where(x => x.UserTgId == tgId)
                    .Where(x => x.ArrivalСity == null)
                    .FirstOrDefault();

                flight.ArrivalСity = cityFromMessage;

                await botClient.SendTextMessageAsync(tgId, $"город прибытия {cityFromMessage}, " +
                                                           "теперь введите дату отправления в формате  XX.XX.XXXX:");
                user.InputState = InputState.DepartureDate;
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(tgId, "Город прибытия не найден - повторите ввод:");
                user.InputState = InputState.ArrivalСity;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////// ПАРСИМ ДАТУ ВЫЛЕТА
        if (user.InputState == InputState.DepartureDate)
        {
            var flight = FlightsList.flights
                .Where(x => x.UserTgId == tgId)
                .Where(x => x.DepartureDate == null)
                .FirstOrDefault();

            flight.DepartureDate = CitiesCollection.cities.Where(x => x.ToLower().Contains(""));
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
        var fly = new Fly(tgId);
        FlightsList.flights.Add(fly);
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
// Сделать обертку всем методам botclient.SendMessageTextAsync
//     и callback методам

async Task<string> GetFinalTickets(Fly fly)
{
    return "";
}