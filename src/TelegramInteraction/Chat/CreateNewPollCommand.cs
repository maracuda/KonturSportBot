using System.Threading.Tasks;

using BusinessLogic.CreatePolls;

using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramInteraction.Chat
{
    public class CreateNewPollCommand : IChatCommand
    {
        private readonly ITelegramBotClient telegramBotClient;
        private readonly ICreatePollService createPollService;

        public CreateNewPollCommand(ITelegramBotClient telegramBotClient, ICreatePollService createPollService)
        {
            this.telegramBotClient = telegramBotClient;
            this.createPollService = createPollService;
        }

        public async Task ExecuteAsync(Message message)
        {
            await createPollService.CreateAsync(message.Chat.Id, message.From.Id);
            await telegramBotClient.SendTextMessageAsync(message.Chat.Id, "Send me a poll you want to schedule");
        }

        public string[] SupportedTemplates => new[] { "/new" };

        public CommandType Type => CommandType.Text;
    }
}