# AlRP - Discord Rich Presence

A cross-platform Discord Rich Presence manager built with Avalonia UI and .NET.

## Features

- Customize Discord activity status
- Set custom details and state
- Multiple timestamp options (none, since start, local time, custom)
- Support for large and small images
- Custom button with URL
- Cross-platform (Windows, macOS, Linux)
- Modern UI with Discord's dark theme

## Prerequisites

- .NET 9.0 SDK
- Discord desktop client
- Discord application (for Client ID)

## Setup

1. Create a Discord Application:
    - Go to [Discord Developer Portal](https://discord.com/developers/applications)
    - Click "New Application" and give it a name
    - Note down the "Application ID" (Client ID)
    - Optional: Add images in the "Rich Presence" -> "Art Assets" section

2. Build and Run:
   ```bash
   dotnet restore
   dotnet run
   ```

## Usage

1. Enter your Discord Application ID in the "ID" field
2. Choose activity type (Playing, Streaming, etc.)
3. Set your details and state
4. Choose timestamp option:
    - None
    - Since start
    - Since last update
    - Local time
    - Custom timestamp
5. Add image keys if you've uploaded images to your Discord application
6. Add a button with text and URL (optional)
7. Click "Connect" to initialize
8. Click "Update Presence" to apply changes

## Building from Source

```bash
git clone https://github.com/YourUsername/AlPC.git
cd AlPC
dotnet restore
dotnet build
dotnet run
```

## Project Structure

```
AlPC/
├── Models/
│   └── PresenceModel.cs       # Data model for Discord presence
├── Services/
│   ├── IDiscordService.cs     # Discord service interface
│   └── DiscordService.cs      # Discord RPC implementation
├── Views/
│   ├── MainWindow.axaml       # Main window UI
│   ├── MainWindow.axaml.cs    # Main window code-behind
│   └── MainWindowViewModel.cs # Main view model
└── App.axaml                  # Application entry point
```

## Dependencies

- [Avalonia UI](https://avaloniaui.net/) - Cross-platform UI framework
- [Discord Rich Presence](https://github.com/Lachee/discord-rpc-csharp) - Discord RPC library
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) - MVVM utilities

## License

[MIT License](LICENSE)

## Acknowledgments

- Discord for the Rich Presence API
- Avalonia UI team
- DiscordRPC library maintainers