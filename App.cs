using RunningEventTracker.Services;
using System;
using System.Windows.Forms;

namespace RunningEventTracker
{
    static class App
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Передача экземпляров сервисов в конструктор MainForm
            var raceTimer = new RaceTimer();
            var lapProcessor = new LapProcessor();
            var actionHistory = new ActionHistory();

            Application.Run(new MainForm(raceTimer, lapProcessor, actionHistory));
        }
    }
}