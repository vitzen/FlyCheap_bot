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
        public static void Main()
        {
            var botClient = new TelegramBotClient("5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE");
            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };
            botClient.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cts)
        {
            var message = update.Message;
            if (message.Text != null)
            {
                if (message.Text.ToLower().Contains("Привет"))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id,
                        "Привет, это сервис для поиска дешевых авиарейсов");
                    return;
                }
            }
        }


        private static Task Error(ITelegramBotClient botClient, Exception e, CancellationToken cts)
        {
            throw new NotImplementedException();
        }
    }
}