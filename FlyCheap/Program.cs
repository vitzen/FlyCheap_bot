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

        public static void Main()
        {
            // Тест коннекта с ботом TG
            botClient = new TelegramBotClient("5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Тест коннекта с ботом телеграмма \n" +
                              $"i'm user {me.Id} \n" +
                              $"my name is {me.FirstName} \n" +
                              $"username {me.Username}");

            StartReceiver();
            Console.ReadLine();
        }

        /// <summary>
        /// Метод SatrtReciever для запуска telegram бота
        /// </summary>
        public static async Task StartReceiver()
        {
            var token = new CancellationTokenSource();
            var canceltoken = token.Token;
            var ReOpt = new ReceiverOptions() { AllowedUpdates = { } };
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
                SendMessage(message);
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
                await botClient.SendTextMessageAsync("", e.Message.ToString());
            }
        }

        /// <summary>
        /// Метод посыла сообщения от бота (ответ олт бота)
        /// </summary>
        /// <param name="message"></param>
        public static async Task SendMessage(Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hello my Friend, I'm FlyCheap Service");
        }
    }
}