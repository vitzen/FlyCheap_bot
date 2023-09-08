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
            // Тест коннекта с ботом TG
            var botClient = new TelegramBotClient("5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE");
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Тест коннекта с ботом телеграмма \n" +
                              $"i'm user {me.Id} \n" +
                              $"my name is {me.FirstName} \n" +
                              $"username {me.Username}");
            
            

            //     using CancellationTokenSource cts = new();
            //
            //     // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            //     ReceiverOptions receiverOptions = new()
            //     {
            //         AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            //     };
            //     botClient.StartReceiving(Update, Error);
            //
            //     Console.ReadLine();
            // }
            //
            // async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cts)
            // {
            //     var message = update.Message;
            //     if (message.Text != null)
            //     {
            //         if (message.Text.ToLower().Contains("Привет"))
            //         {
            //             await botClient.SendTextMessageAsync(message.Chat.Id,
            //                 "Привет, это сервис для поиска дешевых авиарейсов");
            //             return;
            //         }
            //     }
            // }
            //
            //
            // private static Task Error(ITelegramBotClient botClient, Exception e, CancellationToken cts)
            // {
            //     throw new NotImplementedException();
            // }


            // public static async Task Main()
            // {
            //     var botClient = new TelegramBotClient("5880963661:AAGGZLU75KJbrE_k-JPRckvCTR9ainZL1wE");
            //
            //     using CancellationTokenSource cts = new();
            //
            //     // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            //     ReceiverOptions receiverOptions = new()
            //     {
            //         AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            //     };
            //
            //     botClient.StartReceiving(
            //         updateHandler: HandleUpdateAsync,
            //         pollingErrorHandler: HandlePollingErrorAsync,
            //         receiverOptions: receiverOptions,
            //         cancellationToken: cts.Token
            //     );
            //
            //     var me = await botClient.GetMeAsync();
            //
            //     Console.WriteLine($"Start listening for @{me.Username}");
            //     Console.ReadLine();
            //
            //     // Send cancellation request to stop bot
            //     cts.Cancel();
            //
            //     async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            //         CancellationToken cancellationToken)
            //     {
            //         // Only process Message updates: https://core.telegram.org/bots/api#message
            //         if (update.Message is not { } message)
            //             return;
            //         // Only process text messages
            //         if (message.Text is not { } messageText)
            //             return;
            //
            //         var chatId = message.Chat.Id;
            //
            //         Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
            //
            //         // Echo received message text
            //         Message sentMessage = await botClient.SendTextMessageAsync(
            //             chatId: chatId,
            //             text: "You said:\n" + messageText,
            //             cancellationToken: cancellationToken);
            //     }
            //
            //     Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
            //         CancellationToken cancellationToken)
            //     {
            //         var ErrorMessage = exception switch
            //         {
            //             ApiRequestException apiRequestException
            //                 => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            //             _ => exception.ToString()
            //         };
            //
            //         Console.WriteLine(ErrorMessage);
            //         return Task.CompletedTask;
            //     }

            Console.ReadKey();
        }
    }
}