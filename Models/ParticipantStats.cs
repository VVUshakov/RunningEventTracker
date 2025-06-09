namespace RunningEventTracker.Models
{
    /// <summary>
    /// Содержит агрегированные показатели по одному участнику.
    /// </summary>
    public class ParticipantStats
    {
        /// <summary>
        /// Идентификатор участника.
        /// </summary>
        public int Participant { get; set; }

        /// <summary>
        /// Количество завершённых кругов.
        /// </summary>
        public int LapCount { get; set; }

        /// <summary>
        /// Общая сумма секунд, затраченных на участие.
        /// </summary>
        public double TotalSeconds { get; set; }

        /// <summary>
        /// Среднее время прохождения одного круга в секундах.
        /// </summary>
        public double AvgTime { get; set; }
    }
}
