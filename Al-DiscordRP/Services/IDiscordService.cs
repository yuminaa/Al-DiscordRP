using System;
using System.Threading.Tasks;
using Al_DiscordRP.Models;

namespace Al_DiscordRP.Services;

public interface IDiscordService
{
    bool IsConnected { get; }
    event EventHandler<string> OnStatusUpdate;
    Task ConnectAsync(string clientId);
    Task DisconnectAsync();
    Task UpdatePresenceAsync(PresenceModel model);
}