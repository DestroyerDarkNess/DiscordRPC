Imports System.Runtime.InteropServices
Imports DiscordRPC

Public Class DiscordRegister


    <DllImport(DLL_NAME,
               EntryPoint:="Discord_Register",
               CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Sub Discord_Register(ByVal ApplicationID As String,
                                        ByVal Command As String)
    End Sub

    <DllImport(DLL_NAME,
                EntryPoint:="Discord_RegisterSteamGame",
                CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Sub Discord_RegisterSteamGame(ByVal ApplicationID As String,
                                                ByVal SteamID As String)
    End Sub

End Class
