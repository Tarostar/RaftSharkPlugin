# Raft Shark Plugin

A small BepInEx plugin to mod the Raft game with more sharks

# BepInEx

[BepInEx](https://docs.bepinex.dev)

Download and unzip in Raft directory: [Mono Unity BepInEx](https://docs.bepinex.dev/master/articles/user_guide/installation/unity_mono.html).

Run Raft once to generate log with Unity info, etc.

# Steps I did

I used Visual Studio 2022 Community

## To generate templates

dotnet new -i BepInEx.Templates::2.0.0-be.2 --nuget-source https://nuget.bepinex.dev/v3/index.json

## To create the initial plugin

Note that my Raft version for Unity was 2019.3.5 and used .Net net46.

dotnet new bep6plugin_unity_mono -n RaftSharkPlugin -T net46 -U 2019.3.5

dotnet restore RaftSharkPlugin

dotnet build

From here can build in visual studio.

Note: Remember to change Visual Studio build to Release version

# Libraries

In the root create a libs folder and add the following dlls.

From Raft copy:

```
Assembly-CSharp.dll
Assembly-CSharp-firstpass.dll
UnityEngine.CoreModule.dll
UnityEngine.dll
```

From BepInEx under Raft copy:

```
0Harmony.dll
BepInEx.Core.dll
```

## Add DLL references 

1. Right-click Dependencies under the project and "Add Assembly References..."
2. Choose Browse, 
3. Click "Browse..."
4. Select all dlls in libs folder
5. Click ok until they are added to project as PackageReference in csproj.

# ILSpy

In Visual Studio extensions downloaded ILSpy

Used Tools->IlSpy to open Assembly-CSharp.dll

# Documentation

[Harmony](https://harmony.pardeike.net/articles/intro.html)

[HarmonyX](https://github.com/BepInEx/HarmonyX)
