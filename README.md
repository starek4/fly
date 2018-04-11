# Fly
Remotely control your device from a website or mobile application. You can shutdown, restart or sleep any device with Fly client. Windows devices also support mute.

## Using fly
You need to register on [Fly website](https://fly.starekit.cz/) first. As soon as you have a valid account you are able to log into any Fly client. If you have running Fly client on some device, you can shut down your devices from Fly website or from the mobile application.

## Supported platforms
You can download installers or packages for all platforms from [releases](https://github.com/starek4/fly/releases/latest).

### Actionable clients
Clients that perform shutdown, restart or mute commands. Windows client also support mute command.

#### Windows
All system versions with .NET Framework 4.6 installed.

#### Linux x64
All x64 distributions with **bash**, **uname** and **shutdown** command.<br />
**Install instructions**: Unzip downloaded file and run install.sh script.<br />
**Run client**: fly -l \<login\>

#### OSX x64
macOS Sierra and higher<br />
**Install instructions**: Unzip downloaded file and run install.sh script.<br />
**Run client as root**: sudo fly -l \<login\>

### Actionless clients
Clients that evoke commands on actionable clients.

#### Android
Android Lollipop (API 21) or higher.

#### iOS
iOS 11.1 or higher.

#### Windows 10 (UWP)
UWP build 16299 or higher.

#### WWW
You can also evoke any command from [Fly website](https://fly.starekit.cz/).

## How do Fly clients look like?
![Fly clients](https://starekit.cz/git/fly.jpg)

## Fly architecture
![Fly architecture](https://starekit.cz/git/fly_architecture_m.png)
