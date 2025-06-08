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
        }

        private void InitializeTimer()
        {
            countdownTimer = new System.Timers.Timer(1000); // 1 second interval
            countdownTimer.Elapsed += OnTimedEvent;
            countdownTimer.AutoReset = true;
        }

        private void SetupDataGrids()
        {
            // Setup Data tab
            dataGridViewData.Columns.Add("Participant", "Участник");
            dataGridViewData.Columns.Add("TotalSeconds", "Общее время (сек)");
            dataGridViewData.Columns.Add("LapNumber", "Номер круга");
            dataGridViewData.Columns.Add("RecordTime", "Время фиксации");

            // Setup Rating tab
            dataGridViewRating.Columns.Add("Participant", "Участник");
            dataGridViewRating.Columns.Add("LapCount", "Кругов");
            dataGridViewRating.Columns.Add("TotalTime", "Общее время");
            dataGridViewRating.Columns.Add("AvgTime", "Среднее время круга");
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
                        MessageBox.Show("Время вышло!", "Забег завершен",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if(isTimerRunning) return;

            if(!int.TryParse(txtMinutes.Text, out int minutes) || minutes <= 0)
            {
                MessageBox.Show("Введите положительное число минут!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            totalTime = TimeSpan.FromMinutes(minutes);
            remainingTime = totalTime;
            startTime = DateTime.Now;

            lblTimer.Text = remainingTime.ToString(@"hh\:mm\:ss");
            isTimerRunning = true;
            countdownTimer.Start();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if(!isTimerRunning)
            {
                MessageBox.Show("Сначала запустите таймер!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(!int.TryParse(txtParticipant.Text, out int participant) || participant <= 0)
            {
                MessageBox.Show("Введите корректный номер участника!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            txtParticipant.Clear();
            txtParticipant.Focus();

            MessageBox.Show($"Участник {participant}: круг {lapNumber} зафиксирован!",
                            "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            countdownTimer.Stop();
            isTimerRunning = false;
            lblTimer.Text = "00:00:00";
            MessageBox.Show("Таймер остановлен", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDataGrid()
        {
            dataGridViewData.Rows.Clear();
            foreach(var record in lapRecords)
            {
                dataGridViewData.Rows.Add(
                    record.Participant,
                    Math.Round(record.TotalSeconds, 2),
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
                    TotalTime = g.Max(r => r.TotalSeconds),
                    AvgTime = g.Max(r => r.TotalSeconds) / g.Count()
                })
                .OrderByDescending(s => s.LapCount)
                .ThenBy(s => s.TotalTime)
                .ToList();

            foreach(var stat in stats)
            {
                dataGridViewRating.Rows.Add(
                    stat.Participant,
                    stat.LapCount,
                    Math.Round(stat.TotalTime, 2),
                    Math.Round(stat.AvgTime, 2)
                );
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
        public double TotalTime { get; set; }
        public double AvgTime { get; set; }
    }
}