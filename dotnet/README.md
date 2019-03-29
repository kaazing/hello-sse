# Using .NET Core 2.0/.NET Framework 4.7.2 for Server Sent Events

Built/Tested with Visual Studio 2019 Preview under Windows 10.

The `App.config` file in repository specifies .NET Framework 4.7.2, but this sample will also compile/run successfully under .NET Core 2.0.

This sample relies on two packages from NuGet which Visual Studio should install automatically:
- 3v.EvtSource (version 1.1.1 or better) - Implementation of Server-Sent Events standard
- WinHttpHandler (version 4.5.2 or better) - Microsoft package which enables the program to use HTTP/2

## Instructions to Build/Run:
- Open Visual Studio
- Under "File/Open" select "Project/Solution"
- Select `ConsoleSSE.sln`
- Under "Build" select "Build Solution"
- If build fails because Visual Studio didn't install `3v.EvtSource` and/or `WinHttpHandler` automatically:
    - Under "Project" select "Manage NuGet Packages"
    - Under Browse:
        - Search for `3v.EvtSource` and install
        - Search for `WinHttpHandler` and install
    
- Run from Visual Studio:
    - Under "Debug" select "Start Debugging"
- Run from Command Prompt/Power Shell:
    - Navigate to ConsoleSSE\ConsoleSSE\bin\Debug directory
    - Run `.\ConsoleSSE.exe <Server-Sent Events URL>`



