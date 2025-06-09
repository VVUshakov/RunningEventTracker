using RunningEventTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RunningEventTracker.Services
{
    /// <summary>
    /// Сервис для обработки кругов забега.
    /// </summary>
    public class LapProcessor : ILapProcessor
    {
        private readonly List<LapRecord> _laps = new List<LapRecord>();

        /// <summary>
        /// Зарегистрирует очередной круг для конкретного участника.
        /// </summary>
        public void ProcessLap(int participantId)
        {
            var lap = new LapRecord
            {
                Participant = participantId,
                TotalSeconds = /* рассчитать общее время */,
                LapNumber = CalculateLapNumber(participantId),
                RecordTime = DateTime.Now
            };

            _laps.Add(lap);
        }

        /// <summary>
        /// Получает список всех зарегистрированных кругов.
        /// </summary>
        public IEnumerable<LapRecord> GetAllLaps()
        {
            return _laps.AsEnumerable();
        }

        /// <summary>
        /// Вычисляет порядковый номер круга для участника.
        /// </summary>
        private int CalculateLapNumber(int participantId)
        {
            return _laps.Where(x => x.Participant == participantId).Count() + 1;
        }
    }
}