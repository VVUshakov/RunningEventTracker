using System;
using System.Timers;

namespace RunningEventTracker.Services
{
    /// <summary>
    /// Реализует функциональность обратного отсчёта.
    /// </summary>
    public class RaceTimer : IRaceTimer
    {
        private readonly Timer _timer;

        public RaceTimer()
        {
            _timer = new Timer(1000); // интервал обновлений раз в секунду
            _timer.Tick += OnTick;
        }

        /// <summary>
        /// Триггерированное событие при каждом новом тике таймера.
        /// </summary>
        public event EventHandler<EventArgs> Tick;

        /// <summary>
        /// Метод, вызванный при наступлении очередного интервала.
        /// </summary>
        protected virtual void OnTick(object sender, EventArgs args)
        {
            Tick?.Invoke(this, args);
        }

        /// <summary>
        /// Начало отсчёта таймера.
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        /// Завершение работы таймера.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }
    }
}