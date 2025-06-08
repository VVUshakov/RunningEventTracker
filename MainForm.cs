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
        private System.Timers.Timer countdownTimer;
        private TimeSpan remainingTime;
        private TimeSpan totalTime;
        private bool isTimerRunning = false;
        private List<LapRecord> lapRecords = new List<LapRecord>();
        private DateTime startTime;

        public MainForm()
        {
            InitializeComponent();
            InitializeTimer();
            SetupDataGrids();
            SetupRecentRecordsGrid();
        }

        private void InitializeTimer()
        {
            countdownTimer = new System.Timers.Timer(1000);
            countdownTimer.Elapsed += OnTimedEvent;
            countdownTimer.AutoReset = true;
        }

        private void SetupDataGrids()
        {
            // Setup Data tab
            dataGridViewData.Columns.Add("Participant", "Участник");
            dataGridViewData.Columns.Add("TotalSeconds", "Общее время (сек)");
            dataGridViewData.Columns.Add("Minutes", "Время (мин)");
            dataGridViewData.Columns.Add("LapNumber", "Номер круга");
            dataGridViewData.Columns.Add("RecordTime", "Время фиксации");

            // Setup Rating tab
            dataGridViewRating.Columns.Add("Participant", "Участник");
            dataGridViewRating.Columns.Add("LapCount", "Кругов");
            dataGridViewRating.Columns.Add("TotalSeconds", "Общее время (сек)");
            dataGridViewRating.Columns.Add("TotalMinutes", "Общее время (мин)");
            dataGridViewRating.Columns.Add("AvgTime", "Среднее время круга (мин)");
        }

        private void SetupRecentRecordsGrid()
        {
            dataGridViewRecent.Columns.Add("Time", "Время");
            dataGridViewRecent.Columns.Add("Info", "Информация");
            dataGridViewRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewRecent.RowHeadersVisible = false;
            dataGridViewRecent.AllowUserToAddRows = false;
            dataGridViewRecent.ReadOnly = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if(isTimerRunning)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));

                if(remainingTime.TotalSeconds <= 0)
                {
                    countdownTimer.Stop();
                    isTimerRunning = false;
                    remainingTime = TimeSpan.Zero;

                    Invoke(new Action(() =>
                    {
                        lblTimer.Text = "00:00:00";
                        AddRecentRecord("Время вышло!");
                    }));
                    return;
                }

                Invoke(new Action(() =>
                {
                    lblTimer.Text = remainingTime.ToString(@"hh\:mm\:ss");
                }));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void StartTimer()
        {
            if(isTimerRunning) return;

            if(!int.TryParse(txtMinutes.Text, out int minutes) || minutes <= 0)
            {
                AddRecentRecord("Ошибка: некорректное время");
                txtMinutes.Clear();
                txtMinutes.Focus();
                return;
            }

            totalTime = TimeSpan.FromMinutes(minutes);
            remainingTime = totalTime;
            startTime = DateTime.Now;

            lblTimer.Text = remainingTime.ToString(@"hh\:mm\:ss");
            isTimerRunning = true;
            countdownTimer.Start();
            AddRecentRecord($"Отсчет начат: {minutes} мин");
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            RecordLap();
        }

        private void RecordLap()
        {
            if(!isTimerRunning)
            {
                AddRecentRecord("Ошибка: таймер не запущен");
                return;
            }

            if(!int.TryParse(txtParticipant.Text, out int participant) || participant <= 0)
            {
                AddRecentRecord("Ошибка: некорректный участник");
                txtParticipant.Clear();
                txtParticipant.Focus();
                return;
            }

            TimeSpan currentTime = DateTime.Now - startTime;
            int lapNumber = lapRecords
                .Where(r => r.Participant == participant)
                .Count() + 1;

            var record = new LapRecord
            {
                Participant = participant,
                TotalSeconds = currentTime.TotalSeconds,
                LapNumber = lapNumber,
                RecordTime = DateTime.Now
            };

            lapRecords.Add(record);
            UpdateDataGrid();
            UpdateRating();

            AddRecentRecord($"Участник {participant}: круг {lapNumber}");

            txtParticipant.Clear();
            txtParticipant.Focus();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void StopTimer()
        {
            countdownTimer.Stop();
            isTimerRunning = false;
            lblTimer.Text = "00:00:00";
            AddRecentRecord("Таймер остановлен");
        }

        private void UpdateDataGrid()
        {
            dataGridViewData.Rows.Clear();
            foreach(var record in lapRecords)
            {
                dataGridViewData.Rows.Add(
                    record.Participant,
                    Math.Round(record.TotalSeconds, 2),
                    Math.Round(record.TotalSeconds / 60, 2), // Минуты
                    record.LapNumber,
                    record.RecordTime.ToString("dd.MM.yyyy HH:mm:ss")
                );
            }
        }

        private void UpdateRating()
        {
            dataGridViewRating.Rows.Clear();

            var stats = lapRecords
                .GroupBy(r => r.Participant)
                .Select(g => new ParticipantStats
                {
                    Participant = g.Key,
                    LapCount = g.Count(),
                    TotalSeconds = g.Max(r => r.TotalSeconds),
                    AvgTime = g.Max(r => r.TotalSeconds) / g.Count()
                })
                .OrderByDescending(s => s.LapCount)
                .ThenBy(s => s.TotalSeconds)
                .ToList();

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

        private void AddRecentRecord(string message)
        {
            // Добавляем новую запись в начало
            dataGridViewRecent.Rows.Insert(0, DateTime.Now.ToString("HH:mm:ss"), message);

            // Ограничиваем количество записей (5 последних)
            while(dataGridViewRecent.Rows.Count > 5)
            {
                dataGridViewRecent.Rows.RemoveAt(dataGridViewRecent.Rows.Count - 1);
            }
        }

        private void txtParticipant_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                RecordLap();
            }
        }
    }

    public class LapRecord
    {
        public int Participant { get; set; }
        public double TotalSeconds { get; set; }
        public int LapNumber { get; set; }
        public DateTime RecordTime { get; set; }
    }

    public class ParticipantStats
    {
        public int Participant { get; set; }
        public int LapCount { get; set; }
        public double TotalSeconds { get; set; }
        public double AvgTime { get; set; }
    }
}