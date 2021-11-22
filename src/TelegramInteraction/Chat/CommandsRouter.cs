using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInteraction.Chat
{
    public class CommandsRouter : ICommandsRouter
    {
        public CommandsRouter(ITelegramBotClient telegramBotClient,
                              IChatCommand[] commands
        )
        {
            this.telegramBotClient = telegramBotClient;
            this.commands = commands;
        }

        public async Task RouteAsync(Message message)
        {
            var chatCommand = ChooseCommand(message);

            if(chatCommand != null)
            {
                await chatCommand.ExecuteAsync(message);
            }
        }

        private IChatCommand? ChooseCommand(Message message)
        {
            if(message.Poll != null)
            {
                return commands.SingleOrDefault(c => c.Type == CommandType.Poll);
            }

            if(message.Text != null)
            {
                var commandText = message.Text.Split(' ').First();
                return commands.SingleOrDefault(c => c.Type == CommandType.Text
                                                     && c.SupportedTemplates.Contains(commandText)
                );
                
                /*
                if(commandText.StartsWith("/"))
                {
                    await telegramBotClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: $"Не знаю что ответить, попробуй что-то из знакомого:\r\n"
                              + $"{string.Join("\r\n", commands.SelectMany(c => c.SupportedTemplates))}"
                    );
                }*/
            }

            return null;
        }

        private readonly ITelegramBotClient telegramBotClient;
        private readonly IChatCommand[] commands;
    }
}