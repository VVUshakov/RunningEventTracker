using System.Collections.Generic;


namespace RunningEventTracker.Services
{
    /// <summary>
    /// Интерфейс для хранилища истории событий.
    /// </summary>
    public interface IActionHistory
    {
        /// <summary>
        /// Записывает новое сообщение в журнал.
        /// </summary>
        void Log(string message);

        /// <summary>
        /// Предоставляет список недавних сообщений.
        /// </summary>
        IEnumerable<string> RecentLogs();
    }
}