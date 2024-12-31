using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Al_DiscordRP.Models;
using Al_DiscordRP.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Al_DiscordRP.Views;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IDiscordService? _discordService;

    [ObservableProperty]
    private string _statusMessage = "Disconnected";

    [ObservableProperty]
    private string _clientId = string.Empty;
    
    [ObservableProperty]
    private string _selectedType = "Playing";
    
    [ObservableProperty]
    private ObservableCollection<string> _types = new()
    {
        "Playing",
        "Streaming",
        "Listening",
        "Watching",
        "Competing"
    };
    
    [ObservableProperty]
    private string _details = string.Empty;
    
    [ObservableProperty]
    private string _state = string.Empty;
    
    [ObservableProperty]
    private bool _noTimestamp = true;
    
    [ObservableProperty]
    private bool _sinceStarted;
    
    [ObservableProperty]
    private bool _sinceUpdate;
    
    [ObservableProperty]
    private bool _localTime;
    
    [ObservableProperty]
    private bool _customTimestamp;
    
    [ObservableProperty]
    private DateTime? _selectedDate;
    
    [ObservableProperty]
    private TimeSpan? _selectedTime;
    
    [ObservableProperty]
    private string _largeImage = string.Empty;
    
    [ObservableProperty]
    private string _smallImage = string.Empty;
    
    [ObservableProperty]
    private string _button1Text = string.Empty;
    
    [ObservableProperty]
    private string _button1Url = string.Empty;

    public MainWindowViewModel()
    {
        // Default constructor
    }

    public MainWindowViewModel(IDiscordService discordService)
    {
        _discordService = discordService ?? throw new ArgumentNullException(nameof(discordService));
        _discordService.OnStatusUpdate += (_, message) => StatusMessage = message;
    }

    [RelayCommand]
    private async Task ConnectAsync()
    {
        if (_discordService == null) return;
        
        try
        {
            await _discordService.ConnectAsync(ClientId);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Connection failed: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task DisconnectAsync()
    {
        if (_discordService == null) return;
        
        try
        {
            await _discordService.DisconnectAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Disconnect failed: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task UpdatePresenceAsync()
    {
        if (_discordService == null) return;
        
        try
        {
            var model = new PresenceModel
            {
                ClientId = ClientId,
                ActivityType = SelectedType,
                Details = Details,
                State = State,
                TimestampType = GetSelectedTimestampType(),
                CustomTimestamp = GetCustomTimestamp(),
                LargeImageKey = LargeImage,
                SmallImageKey = SmallImage,
                ButtonText = Button1Text,
                ButtonUrl = Button1Url
            };

            await _discordService.UpdatePresenceAsync(model);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Update failed: {ex.Message}";
        }
    }

    private TimestampType GetSelectedTimestampType()
    {
        if (NoTimestamp) return TimestampType.None;
        if (SinceStarted) return TimestampType.SinceStart;
        if (SinceUpdate) return TimestampType.SinceUpdate;
        if (LocalTime) return TimestampType.LocalTime;
        if (CustomTimestamp) return TimestampType.Custom;
        return TimestampType.None;
    }

    private DateTime? GetCustomTimestamp()
    {
        if (!CustomTimestamp || !SelectedDate.HasValue || !SelectedTime.HasValue)
            return null;

        return SelectedDate.Value.Date + SelectedTime.Value;
    }
}