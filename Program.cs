using Microsoft.Extensions.DependencyInjection;
using RunningEventTracker;
using RunningEventTracker.Services;
using System;
using System.Windows.Forms;


class Program
{
    static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IRaceTimer, RaceTimer>();
        services.AddSingleton<ILapProcessor, LapProcessor>();
        services.AddSingleton<IActionHistory, ActionHistory>();
        services.AddTransient<MainForm>();
        return services.BuildServiceProvider();
    }

    [STAThread]
    static void Main2()
    {
        var serviceProvider = ConfigureServices();
        using(var mainForm = serviceProvider.GetRequiredService<MainForm>())
        {
            Application.Run(mainForm);
        }
    }
}