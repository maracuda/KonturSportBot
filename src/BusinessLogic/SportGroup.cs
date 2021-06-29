﻿namespace BusinessLogic
{
    public class SportGroup
    {
        /// <summary>
        /// Расписание занятий в Cron формате
        /// </summary>
        public string TrainingSchedule { get; set; }

        /// <summary>
        /// Id чата в телеграмме
        /// </summary>
        public string TelegramChatId { get; set; }

        /// <summary>
        /// Имя группы
        /// </summary>
        public string Name { get; set; }
    }
}