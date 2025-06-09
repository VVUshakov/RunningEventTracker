using System;


namespace RunningEventTracker.Models
{
    /// <summary>
    /// Представляет один зарегистрированный круг участия бегуна.
    /// </summary>
    public class LapRecord
    {
        /// <summary>
        /// Идентификатор участника забега.
        /// </summary>
        public int Participant { get; set; }

        /// <summary>
        /// Общее время прохождения кругов в секундах.
        /// </summary>
        public double TotalSeconds { get; set; }

        /// <summary>
        /// Порядковый номер круга (начиная с 1).
        /// </summary>
        public int LapNumber { get; set; }

        /// <summary>
        /// Дата и время регистрации круга.
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}