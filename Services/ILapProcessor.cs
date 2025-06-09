using System.Collections.Generic;

namespace RunningEventTracker.Services
{
    /// <summary>
    /// Интерфейс для сервиса обработки зарегистрированных кругов.
    /// </summary>
    public interface ILapProcessor
    {
        /// <summary>
        /// Регистрация нового круга для указанного участника.
        /// </summary>
        void ProcessLap(int participantId);

        /// <summary>
        /// Возвращает список всех зарегистрированных кругов.
        /// </summary>
        IEnumerable<LapRecord> GetAllLaps();
    }
}
