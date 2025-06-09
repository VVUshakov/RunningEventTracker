using System;

namespace RunningEventTracker.Services
{
    /// <summary>
    /// Интерфейс для таймера, позволяющего начать обратный отсчёт и останавливаться.
    /// </summary>
    public interface IRaceTimer
    {
        /// <summary>
        /// Событие, срабатывающее каждые заданные интервалы времени.
        /// </summary>
        event EventHandler<EventArgs> Tick;

        /// <summary>
        /// Начинает работу таймера.
        /// </summary>
        void Start();

        /// <summary>
        /// Останавливает таймер.
        /// </summary>
        void Stop();
    }
}