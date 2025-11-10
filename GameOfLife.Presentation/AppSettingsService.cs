using System;
using GameOfLife.Domain;

namespace GameOfLife.Presentation;

public static class AppSettingsService
{
    public static AppSettings Current { get; set; } = new AppSettings();
    
    public static event Action SettingsChanged = delegate { };

    public static void UpdateSettings(AppSettings settings)
    {
        Current = settings;
        SettingsChanged?.Invoke();
    }
}
