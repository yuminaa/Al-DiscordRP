using System;

namespace Al_DiscordRP.Models;

public class PresenceModel
{
    public string ClientId { get; set; } = string.Empty;
    public string ActivityType { get; set; } = "Playing";
    public string Details { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public TimestampType TimestampType { get; set; } = TimestampType.None;
    public DateTime? CustomTimestamp { get; set; }
    public string LargeImageKey { get; set; } = string.Empty;
    public string SmallImageKey { get; set; } = string.Empty;
    public string ButtonText { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = string.Empty;
}

public enum TimestampType
{
    None,
    SinceStart,
    SinceUpdate,
    LocalTime,
    Custom
}