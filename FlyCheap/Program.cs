// Telegram bot logic for SkyCheap project

using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace FlyCheap
{
    class Program
    {
        private static TelegramBotClient botClient;

        public static async Task Main()
        {
            // Тест коннекта с ботом TG
            botClient = new TelegramBotClient("5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE");
            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Тест коннекта с ботом телеграмма \n" +
                              $"i'm user {me.Id} \n" +
                              $"my name is {me.FirstName} \n" +
                              $"username {me.Username}");

            await StartReceiver();

            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "flight search" },
                new KeyboardButton[] { "favorites️" },
            })
            {
                ResizeKeyboard = true
            };

            // Message sentMessage = await botClient.SendTextMessageAsync(
            //     chatId: null,
            //     text: "Choose a response",
            //     replyMarkup: replyKeyboardMarkup,
            //     cancellationToken: null);

            Console.ReadLine();
        }

        /// <summary>
        /// Метод SatrtReciever для запуска telegram бота
        /// </summary>
        public static async Task StartReceiver()
        {
            var token = new CancellationTokenSource();
            var canceltoken = token.Token;
            var ReOpt = new ReceiverOptions() { AllowedUpdates = { }, Limit = 99 };
            await botClient.ReceiveAsync(OnMessage, ErrorMessage, ReOpt, canceltoken);
        }

        /// <summary>
        /// Метод OnMessage
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cts"></param>
        public static async Task OnMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellation)
        {
            if (update.Message is Message message)
            {
                await SendMessage(message);
            }
        }

        /// <summary>
        /// Метод вывода ошибок Error
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="e"></param>
        /// <param name="cts"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static async Task ErrorMessage(ITelegramBotClient telegramBot, Exception e,
            CancellationToken cancelation)
        {
            if (e is ApiRequestException requestException)
            {
                //await botClient.SendTextMessageAsync("", e.Message.ToString());
            }
            Console.WriteLine($"{e.Message}");
        }

        /// <summary>
        /// Метод посыла сообщения от бота (ответ олт бота)
        /// </summary>
        /// <param name="message"></param>
        public static async Task SendMessage(Message message, ReplyKeyboardMarkup replyKeyboardMarkup = null)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hello my Friend, I'm FlyCheap Service",
                replyMarkup: replyKeyboardMarkup);
        }
    }
}