using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

namespace RunningEventTracker
{
    public partial class MainForm : Form
    {
        #region Поля класса

        /// <summary>
        /// Таймер для обратного отсчета
        /// </summary>
        private System.Timers.Timer countdownTimer;

        /// <summary>
        /// Оставшееся время до окончания забега
        /// </summary>
        private TimeSpan remainingTime;

        /// <summary>
        /// Общее время забега в минутах
        /// </summary>
        private TimeSpan totalTime;

        /// <summary>
        /// Флаг активности таймера
        /// </summary>
        private bool isTimerRunning = false;

        /// <summary>
        /// Коллекция зафиксированных кругов участников
        /// </summary>
        private List<LapRecord> lapRecords = new List<LapRecord>();

        /// <summary>
        /// Время начала текущего забега
        /// </summary>
        private DateTime startTime;

        #endregion

        #region Конструктор класса
        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
            SetupDataGrids();
            SetupRecentRecordsGrid();
        }

        #endregion

        #region Методы инициализации

        /// <summary>
        /// Инициализирует системный таймер
        /// </summary>
        private void InitializeTimer()
        {
            countdownTimer = new System.Timers.Timer(1000); // Интервал 1 секунда
            countdownTimer.Elapsed += OnTimedEvent;         // Подписка на событие
            countdownTimer.AutoReset = true;                // Автоповтор
        }

        /// <summary>
        /// Настраивает таблицы данных
        /// </summary>
        private void SetupDataGrids()
        {
            // Настройка таблицы данных о кругах
            dataGridViewData.Columns.Add("Participant", "Участник");
            dataGridViewData.Columns.Add("TotalSeconds", "Общее время (сек)");
            dataGridViewData.Columns.Add("Minutes", "Время (мин)");
            dataGridViewData.Columns.Add("LapNumber", "Номер круга");
            dataGridViewData.Columns.Add("RecordTime", "Время фиксации");

            // Настройка таблицы рейтинга
            dataGridViewRating.Columns.Add("Participant", "Участник");
            dataGridViewRating.Columns.Add("LapCount", "Кругов");
            dataGridViewRating.Columns.Add("TotalSeconds", "Общее время (сек)");
            dataGridViewRating.Columns.Add("TotalMinutes", "Общее время (мин)");
            dataGridViewRating.Columns.Add("AvgTime", "Среднее время круга (мин)");
        }

        /// <summary>
        /// Настраивает таблицу последних действий
        /// </summary>
        private void SetupRecentRecordsGrid()
        {
            dataGridViewRecent.Columns.Add("Time", "Время");
            dataGridViewRecent.Columns.Add("Info", "Информация");
            dataGridViewRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewRecent.RowHeadersVisible = false;
            dataGridViewRecent.AllowUserToAddRows = false;
            dataGridViewRecent.ReadOnly = true;
        }

        #endregion

        #region Обработчики таймера

        /// <summary>
        /// Обработчик события таймера (вызывается каждую секунду)
        /// </summary>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if(isTimerRunning)
            {
                // Уменьшаем оставшееся время
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));

                // Проверка окончания времени
                if(remainingTime.TotalSeconds <= 0)
                {
                    // Остановка таймера
                    countdownTimer.Stop();
                    isTimerRunning = false;
                    remainingTime = TimeSpan.Zero;

                    // Обновление интерфейса
                    Invoke(new Action(() =>
                    {
                        lblTimer.Text = "00:00:00";
                        AddRecentRecord("Время вышло!");
                    }));
                    return;
                }

                // Обновление отображения таймера
                Invoke(new Action(() =>
                {
                    lblTimer.Text = remainingTime.ToString(@"hh\:mm\:ss");
                }));
            }
        }

        #endregion

        #region Основные функции управления

        /// <summary>
        /// Запускает таймер обратного отсчета
        /// </summary>
        private void StartTimer()
        {
            if(isTimerRunning) return;

            // Проверка корректности введенного времени
            if(!int.TryParse(txtMinutes.Text, out int minutes) || minutes <= 0)
            {
                AddRecentRecord("Ошибка: некорректное время");
                txtMinutes.Clear();
                txtMinutes.Focus();
                return;
            }

            // Установка времени и запуск
            totalTime = TimeSpan.FromMinutes(minutes);
            remainingTime = totalTime;
            startTime = DateTime.Now;

            lblTimer.Text = remainingTime.ToString(@"hh\:mm\:ss");
            isTimerRunning = true;
            countdownTimer.Start();
            AddRecentRecord($"Отсчет начат: {minutes} мин");
        }

        /// <summary>
        /// Фиксирует пройденный круг для участника
        /// </summary>
        private void RecordLap()
        {
            // Проверка активности таймера
            if(!isTimerRunning)
            {
                AddRecentRecord("Ошибка: таймер не запущен");
                return;
            }

            // Проверка корректности номера участника
            if(!int.TryParse(txtParticipant.Text, out int participant) || participant <= 0)
            {
                AddRecentRecord("Ошибка: некорректный участник");
                txtParticipant.Clear();
                txtParticipant.Focus();
                return;
            }

            // Расчет текущего времени
            TimeSpan currentTime = DateTime.Now - startTime;

            // Определение номера круга
            int lapNumber = lapRecords
                .Where(r => r.Participant == participant)
                .Count() + 1;

            // Создание записи
            var record = new LapRecord
            {
                Participant = participant,
                TotalSeconds = currentTime.TotalSeconds,
                LapNumber = lapNumber,
                RecordTime = DateTime.Now
            };

            // Сохранение и обновление интерфейса
            lapRecords.Add(record);
            UpdateDataGrid();
            UpdateRating();

            // Добавление в историю и очистка поля
            AddRecentRecord($"Участник {participant}: круг {lapNumber}");
            txtParticipant.Clear();
            txtParticipant.Focus();
        }

        /// <summary>
        /// Останавливает таймер
        /// </summary>
        private void StopTimer()
        {
            countdownTimer.Stop();
            isTimerRunning = false;
            lblTimer.Text = "00:00:00";
            AddRecentRecord("Таймер остановлен");
        }

        #endregion

        #region Обработчики кнопок

        /// <summary>
        /// Обработчик нажатия кнопки "Начать отсчет"
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Зафиксировать"
        /// </summary>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            RecordLap();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Стоп"
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        #endregion

        #region Методы обновления данных

        /// <summary>
        /// Обновляет таблицу с данными о кругах
        /// </summary>
        private void UpdateDataGrid()
        {
            dataGridViewData.Rows.Clear();
            foreach(var record in lapRecords)
            {
                dataGridViewData.Rows.Add(
                    record.Participant,
                    Math.Round(record.TotalSeconds, 2),
                    Math.Round(record.TotalSeconds / 60, 2), // Конвертация в минуты
                    record.LapNumber,
                    record.RecordTime.ToString("dd.MM.yyyy HH:mm:ss")
                );
            }
        }

        /// <summary>
        /// Обновляет таблицу рейтинга участников
        /// </summary>
        private void UpdateRating()
        {
            dataGridViewRating.Rows.Clear();

            // Группировка данных по участникам
            var stats = lapRecords
                .GroupBy(r => r.Participant)
                .Select(g => new ParticipantStats
                {
                    Participant = g.Key,
                    LapCount = g.Count(),
                    TotalSeconds = g.Max(r => r.TotalSeconds),
                    AvgTime = g.Max(r => r.TotalSeconds) / g.Count()
                })
                // Сортировка по количеству кругов и времени
                .OrderByDescending(s => s.LapCount)
                .ThenBy(s => s.TotalSeconds)
                .ToList();

            // Заполнение таблицы
            foreach(var stat in stats)
            {
                dataGridViewRating.Rows.Add(
                    stat.Participant,
                    stat.LapCount,
                    Math.Round(stat.TotalSeconds, 2),
                    Math.Round(stat.TotalSeconds / 60, 2), // Минуты
                    Math.Round(stat.AvgTime / 60, 2)      // Минуты
                );
            }
        }

        /// <summary>
        /// Добавляет запись в историю действий
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        private void AddRecentRecord(string message)
        {
            // Добавление новой записи в начало таблицы
            dataGridViewRecent.Rows.Insert(0, DateTime.Now.ToString("HH:mm:ss"), message);

            // Ограничение количества записей (последние 5)
            while(dataGridViewRecent.Rows.Count > 5)
            {
                dataGridViewRecent.Rows.RemoveAt(dataGridViewRecent.Rows.Count - 1);
            }
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Обработчик нажатия клавиши в поле ввода участника
        /// </summary>
        private void txtParticipant_KeyDown(object sender, KeyEventArgs e)
        {
            // Фиксация круга при нажатии Enter
            if(e.KeyCode == Keys.Enter)
            {
                RecordLap();
            }
        }

        #endregion        
    }

    /// <summary>
    /// Представляет запись о пройденном круге участника
    /// </summary>
    public class LapRecord
    {
        /// <summary>
        /// Номер участника
        /// </summary>
        public int Participant { get; set; }

        /// <summary>
        /// Общее время с начала забега в секундах
        /// </summary>
        public double TotalSeconds { get; set; }

        /// <summary>
        /// Номер круга (1, 2, 3...)
        /// </summary>
        public int LapNumber { get; set; }

        /// <summary>
        /// Дата и время фиксации результата
        /// </summary>
        public DateTime RecordTime { get; set; }
    }

    /// <summary>
    /// Содержит статистические данные по участнику
    /// </summary>
    public class ParticipantStats
    {
        /// <summary>
        /// Номер участника
        /// </summary>
        public int Participant { get; set; }

        /// <summary>
        /// Общее количество пройденных кругов
        /// </summary>
        public int LapCount { get; set; }

        /// <summary>
        /// Суммарное время забега в секундах
        /// </summary>
        public double TotalSeconds { get; set; }

        /// <summary>
        /// Среднее время прохождения одного круга в секундах
        /// </summary>
        public double AvgTime { get; set; }
    }
}