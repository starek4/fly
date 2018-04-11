# Fly - remotely control your device
Remotely control your device from a website or mobile application. You can shutdown, restart or sleep any device with Fly client. Windows devices also support mute.

## 1 Using fly
You need to register on [Fly website](https://fly.starekit.cz/) first. As soon as you have a valid account you are able to log into any Fly client. If you have running Fly client on some device, you can shut down your devices from Fly website or from the mobile application.

## 2 Supported platforms
You can download installers or packages for all platforms from [releases](https://github.com/starek4/fly/releases/latest).

### 2.1 Actionable clients
Clients that perform shutdown, restart or mute commands. Windows client also support mute command.

#### 2.1.1 Windows
All system versions with .NET Framework 4.6 installed.

#### 2.1.2 Linux x64
All x64 distributions with **bash**, **uname** and **shutdown** command.<br />
**Install instructions**: Unzip downloaded file and run install.sh script.<br />
**Run client**: fly -l \<login\>

#### 2.1.3 OSX x64
macOS Sierra and higher<br />
**Install instructions**: Unzip downloaded file and run install.sh script.<br />
**Run client as root**: sudo fly -l \<login\>

### 2.2 Actionless clients
Clients that evoke commands on actionable clients.

#### 2.2.1 Android
Android Lollipop (API 21) or higher.

#### 2.2.2 iOS
iOS 11.1 or higher.

#### 2.2.3 Windows 10 (UWP)
UWP build 16299 or higher.

#### 2.2.4 WWW
You can also evoke any command from [Fly website](https://fly.starekit.cz/).

## 3 How do Fly clients look like?
![Fly clients](https://starekit.cz/git/fly.jpg)

## 4 Fly architecture
![Fly architecture](https://starekit.cz/git/fly_architecture_m.png)
