using System;
using System.Threading.Tasks;
using Al_DiscordRP.Models;
using DiscordRPC;

namespace Al_DiscordRP.Services;

public sealed class DiscordService : IDiscordService
{
    private DiscordRpcClient? _client;
    private bool _disposed;
    
    public bool IsConnected => _client?.IsInitialized ?? false;
    public event EventHandler<string>? OnStatusUpdate;

    public Task ConnectAsync(string clientId)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        
        _client?.Dispose();
        _client = new DiscordRpcClient(clientId);
        SetupEvents();
        
        if (!_client.Initialize())
        {
            _client.Dispose();
            _client = null;
            throw new InvalidOperationException("Failed to initialize Discord client");
        }

        return Task.CompletedTask;
    }

    private void SetupEvents()
    {
        if (_client == null) return;
        
        _client.OnReady += HandleReady;
        _client.OnPresenceUpdate += HandlePresenceUpdate;
        _client.OnError += HandleError;
    }

    private void HandleReady(object sender, DiscordRPC.Message.ReadyMessage msg) => 
        OnStatusUpdate?.Invoke(this, $"Connected as {msg.User.Username}");

    private void HandlePresenceUpdate(object sender, DiscordRPC.Message.PresenceMessage _) => 
        OnStatusUpdate?.Invoke(this, "Presence updated");

    private void HandleError(object sender, DiscordRPC.Message.ErrorMessage msg) => 
        OnStatusUpdate?.Invoke(this, $"Error: {msg.Message}");

    public Task DisconnectAsync()
    {
        if (_client != null)
        {
            _client.Dispose();
            _client = null;
            OnStatusUpdate?.Invoke(this, "Disconnected");
        }
        return Task.CompletedTask;
    }

    public Task UpdatePresenceAsync(PresenceModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        
        if (_client == null || !IsConnected)
        {
            throw new InvalidOperationException("Not connected to Discord");
        }

        var presence = CreatePresence(model);
        _client.SetPresence(presence);
        
        return Task.CompletedTask;
    }

    private static RichPresence CreatePresence(PresenceModel model)
    {
        var presence = new RichPresence
        {
            Details = model.Details,
            State = model.State,
            Assets = new Assets
            {
                LargeImageKey = model.LargeImageKey,
                SmallImageKey = model.SmallImageKey
            }
        };

        presence.Timestamps = model.TimestampType switch
        {
            TimestampType.SinceStart => Timestamps.Now,
            TimestampType.LocalTime => new Timestamps { Start = DateTime.Now },
            TimestampType.Custom when model.CustomTimestamp.HasValue => 
                new Timestamps { Start = model.CustomTimestamp.Value },
            _ => null
        };

        if (!string.IsNullOrEmpty(model.ButtonUrl))
        {
            presence.Buttons = new[]
            {
                new Button { Label = model.ButtonText, Url = model.ButtonUrl }
            };
        }

        return presence;
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        if (_client != null)
        {
            _client.OnReady -= HandleReady;
            _client.OnPresenceUpdate -= HandlePresenceUpdate;
            _client.OnError -= HandleError;
            _client.Dispose();
            _client = null;
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}