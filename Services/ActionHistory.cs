using System.Collections.Generic;
using System.Linq;


namespace RunningEventTracker.Services
{
    /// <summary>
    /// Служба для хранения недавней истории действий.
    /// </summary>
    public class ActionHistory : IActionHistory
    {
        private readonly Queue<string> _logs = new Queue<string>(5);

        /// <summary>
        /// Создает новую запись в журнале.
        /// </summary>
        public void Log(string message)
        {
            _logs.Enqueue(message);
            if(_logs.Count > 5)
                _logs.Dequeue();
        }

        /// <summary>
        /// Возврат списка недавних записей.
        /// </summary>
        public IEnumerable<string> RecentLogs()
        {
            return _logs.Reverse().AsEnumerable();
        }
    }
}