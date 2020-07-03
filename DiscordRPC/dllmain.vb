Imports System.Reflection
Imports System.IO
Imports DiscordRPC.LogFuncs

Public Class dllmain

    Public Shared CurrentDLLPath As String = Assembly.GetExecutingAssembly().Location
    Public Shared CurrentLocation As String = Path.GetDirectoryName(CurrentDLLPath)

    Public Shared GamePath As String = Assembly.GetCallingAssembly.Location
    Public Shared GameProcName As String = System.IO.Path.GetFileNameWithoutExtension(GamePath)

    Public Shared DLL_Folder As String = "DiscordLib"
    Public Shared INI_Name As String = "Config.ini"

    Public Shared DLLPluginDir As String = CurrentLocation & "\" & DLL_Folder
    Public Shared INIDir As String = CurrentLocation & "\" & DLL_Folder & "\" & INI_Name
    Public Shared processName As String = "NetLoader.exe"

    Public Shared APPLICATION_ID As String = "728629097164701826"
    Public Shared APP_Details As String = "Discord Rich Presence v.01 beta loaded."
    Public Shared APP_State As String = "Plugin For NetLoader."
    Public Shared APP_LargeImageName As String = "neticon"
    Public Shared APP_LargeImageText As String = "Using NetLoader Plugin"

    Public Shared Function Main() As String
        Dim WelcomMessage As String = " Load Your DLLs into SAMP without the need for External Injectors. "
        Dim tskThread As New Task(ScanAsyc, TaskCreationOptions.LongRunning)
        tskThread.Start()
        Return WelcomMessage
    End Function


    Private Shared ScanAsyc As New Action(
  Sub()

      If Not My.Computer.FileSystem.DirectoryExists(DLLPluginDir) = True Then
          My.Computer.FileSystem.CreateDirectory(DLLPluginDir)
      End If

      If My.Computer.FileSystem.FileExists(LogFile) = True Then
          My.Computer.FileSystem.DeleteFile(LogFile)
      End If

      If Not My.Computer.FileSystem.FileExists(INIDir) = True Then
          INI_Manager.Set_Value(INIDir, "APP_ID", APPLICATION_ID) ' Save
          INI_Manager.Set_Value(INIDir, "APP_Details", APP_Details) ' Save
          INI_Manager.Set_Value(INIDir, "APP_State", APP_State) ' Save
          INI_Manager.Set_Value(INIDir, "APP_LargeImageName", APP_LargeImageName) ' Save
          INI_Manager.Set_Value(INIDir, "APP_LargeImageText", APP_LargeImageText) ' Save
          INI_Manager.Sort_Values(INIDir)
      End If

      Dim AppID As String = INI_Manager.Load_Value(INIDir, "APP_ID") ' Load
      Dim AppDetails As String = INI_Manager.Load_Value(INIDir, "APP_Details") ' Load
      Dim AppState As String = INI_Manager.Load_Value(INIDir, "APP_State") ' Load
      Dim AppLargeImageName As String = INI_Manager.Load_Value(INIDir, "APP_LargeImageName") ' Load
      Dim AppLargeImageText As String = INI_Manager.Load_Value(INIDir, "APP_LargeImageText") ' Load

      ' INI_Manager.Delete_Value(".\Test.ini", "TextValue") ' Delete
      ' INI_Manager.Sort_Values(".\Test.ini") ' Sort INI File


      WriteLog("Session started .", LogFuncs.InfoType.Information)
      WriteLog(" ", LogFuncs.InfoType.None)
      WriteLog("Discord Rich Presence v.01 beta loaded.", LogFuncs.InfoType.Information)
      WriteLog("Developers: Destroyer", LogFuncs.InfoType.Information)
      WriteLog("Copyright (c) 2020", LogFuncs.InfoType.Information)
      WriteLog(" ", LogFuncs.InfoType.None)
      WriteLog("Working directory: " & DLLPluginDir, LogFuncs.InfoType.Information)
      WriteLog(" ", LogFuncs.InfoType.None)
      WriteLog("Details = " & AppDetails, LogFuncs.InfoType.None)
      WriteLog("State = " & AppState, LogFuncs.InfoType.None)
      WriteLog("LargeImageKey = " & AppLargeImageName, LogFuncs.InfoType.None)
      WriteLog("LargeImageText = " & AppLargeImageText, LogFuncs.InfoType.None)

      Dim Handlers As DiscordEventHandlers =
          New DiscordEventHandlers With {
          .Ready = AddressOf IsReady
      }

      Discord_Initialize(AppID, Handlers, 1, 0)

      Dim Presence As DiscordRichPresence =
          New DiscordRichPresence With {
              .Details = AppDetails,
              .State = AppState,
              .LargeImageKey = AppLargeImageName,
              .LargeImageText = AppLargeImageText
          }

      Discord_UpdatePresence(Presence)

      ' Code for Cosole APP. *Bucle Process Monitor * OFF Present

      ' For i As Integer = 0 To 2
      ' Dim p As Process() = Process.GetProcessesByName(CorrectProcessString(processName))
      ' If p.Count = 0 Then
      ' WriteLog("The game is Ending!", LogFuncs.InfoType.Information)
      ' Discord_ClearPresence()
      '  Discord_Shutdown()
      ' Exit For
      ' End If
      ' i -= 1
      ' Next

  End Sub)


    Public Shared Sub IsReady(ByRef Request As DiscordUser)
        WriteLog("Discord initialized successfully!", LogFuncs.InfoType.Information)
    End Sub

    Public Shared Function CorrectProcessString(ByVal processA As String) As String
        ' Another method would be :
        ' Dim ProcessName As String = Path.GetFileNameWithoutExtension(processA)
        ' Return ProcessName
        Dim ProcessName As String = processA
        If ProcessName.ToLower.EndsWith(".exe") Then ProcessName = ProcessName.Substring(0, ProcessName.Length - 4)
        Return ProcessName
    End Function

End Class
