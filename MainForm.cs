using RunningEventTracker.Services;
using System;
using System.Windows.Forms;

namespace RunningEventTracker
{
    /// <summary>
    /// Главная форма приложения для отслеживания соревнований.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly IRaceTimer _raceTimer;
        private readonly ILapProcessor _lapProcessor;
        private readonly IActionHistory _actionHistory;

        public MainForm(IRaceTimer raceTimer, ILapProcessor lapProcessor, IActionHistory actionHistory)
        {
            InitializeComponent();
            _raceTimer = raceTimer;
            _lapProcessor = lapProcessor;
            _actionHistory = actionHistory;

            _raceTimer.Tick += OnTimerTick;
        }

        /// <summary>
        /// Метод, выполняемый при каждом тике таймера.
        /// </summary>
        private void OnTimerTick(object sender, EventArgs args)
        {
            // Логика тикера таймера
        }

        /// <summary>
        /// Нажатие кнопки "Начать забег".
        /// </summary>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            _raceTimer.Start();
        }

        /// <summary>
        /// Нажатие кнопки "Зарегистрировать круг".
        /// </summary>
        private void BtnRecord_Click(object sender, EventArgs e)
        {
            // Валидируем входные данные
            if(!int.TryParse(txtParticipant.Text, out int participant))
            {
                MessageBox.Show("Некорректный номер участника");
                return;
            }

            _lapProcessor.ProcessLap(participant);
            _actionHistory.Log($"Участник {participant}: новый круг");
        }

        /// <summary>
        /// Нажатие кнопки "Остановить забег".
        /// </summary>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            _raceTimer.Stop();
        }
    }
}