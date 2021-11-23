using System;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using Vostok.Logging.Abstractions;

namespace TelegramInteraction.Chat
{
    public class ChatWorker
    {
        public ChatWorker(ILog log,
                          ITelegramBotClient telegramBotClient,
                          ICommandsRouter commandsRouter
        )
        {
            this.log = log;
            bot = telegramBotClient;
            this.commandsRouter = commandsRouter;
        }

        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            var me = await bot.GetMeAsync(cancellationToken);
            Console.Title = me.Username;

            bot.OnReceiveError += (_, args) => log.Error(args.ApiRequestException);
            bot.OnUpdate += BotOnMessageReceived;

            bot.StartReceiving(new [] {UpdateType.Message, UpdateType.Poll, UpdateType.CallbackQuery}, cancellationToken);
            log.Info($"Start listening for @{me.Username}");
        }

        private async void BotOnMessageReceived(object sender, UpdateEventArgs e)
        {

            switch(e.Update.Type)
            {
            // e.Update.Message.Chat.Type == ChatType.Private
                // диалоги можно отличить по типу: каналы, чаты 1-1, группы, супер группы
                // нужно сделать одни виды команд для чатов 1-1 для заведения опросов
                // и второй для групп/суперГрупп для добавления опроса
                // Нужны ли опросы в чатах 1-1? 
                // опрос своего настроения- пока непонятно
            case UpdateType.Message:
                ProcessMessage(e.Update.Message);
                break;
            case UpdateType.CallbackQuery:
                Console.WriteLine(e.Update.CallbackQuery.Data);
                break;
            default:
                return;
            }
        }

        private async Task ProcessMessage(Message message)
        {
            if(message == null)
            {
                return;
            }

            await commandsRouter.RouteAsync(message);
        }

        private readonly ILog log;

        private readonly ITelegramBotClient bot;
        private readonly ICommandsRouter commandsRouter;
    }
}