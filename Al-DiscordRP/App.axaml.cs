using Al_DiscordRP.Services;
using Al_DiscordRP.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Al_DiscordRP;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var discordService = new DiscordService();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(discordService)
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}