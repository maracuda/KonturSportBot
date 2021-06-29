﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;

namespace BusinessLogic
{
    public class SendPollWork
    {
        private readonly ITelegramBotClient telegramBotClient;

        public SendPollWork(
            ITelegramBotClient telegramBotClient
            )
        {
            this.telegramBotClient = telegramBotClient;
        }

        public async Task ExecuteAsync(CancellationToken token, string chatId, DateTimeOffset trainingTime)
        {
            await telegramBotClient.SendPollAsync(chatId,
                                                  $"Йога {trainingTime.Date.ToShortDateString()}",
                                                  new[] {"Пойду в зал", "Пойду онлайн", "Не пойду", "Возможно",},
                                                  isAnonymous: false,
                                                  cancellationToken: token
            );
        }
    }
}