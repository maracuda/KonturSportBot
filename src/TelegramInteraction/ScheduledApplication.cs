﻿using System.Threading.Tasks;

using BusinessLogic;

using SimpleInjector;

using Vostok.Applications.Scheduled;
using Vostok.Hosting.Abstractions;

namespace TelegramInteraction
{
    public class ScheduledApplication : VostokScheduledAsyncApplication
    {
        protected override Task SetupAsync(IScheduledActionsBuilder builder, IVostokHostingEnvironment environment)
        {
            var container = environment.HostExtensions.Get<Container>();
            var sendPollWork = container.GetInstance<SendPollWork>();

            builder.Schedule(sendPollWork.GetType().Name,
                             Scheduler.Crontab(
                                 container.GetInstance<IApplicationSettings>().GetString("Schedule")),
                             context => sendPollWork.ExecuteAsync(context.CancellationToken)
            );

            return Task.CompletedTask;
        }
    }
}