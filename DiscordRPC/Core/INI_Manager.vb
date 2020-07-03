

#Region " INI Manager "

' [ INI Manager Functions ]
'
' // By Elektro H@cker
'
' Examples :
'
' INI_Manager.Set_Value(".\Test.ini", "TextValue", TextBox1.Text) ' Save
' TextBox1.Text = INI_Manager.Load_Value(".\Test.ini", "TextValue") ' Load
' INI_Manager.Delete_Value(".\Test.ini", "TextValue") ' Delete
' INI_Manager.Sort_Values(".\Test.ini") ' Sort INI File

Public Class INI_Manager

    ''' <summary>
    ''' The INI File Location.
    ''' </summary>
    Public Shared INI_File As String = dllmain.DLLPluginDir & "\" & "Config.ini"

    ''' <summary>
    ''' Set a value.
    ''' </summary>
    ''' <param name="File">The INI file location</param>
    ''' <param name="ValueName">The value name</param>
    ''' <param name="Value">The value data</param>
    Public Shared Sub Set_Value(ByVal File As String, ByVal ValueName As String, ByVal Value As String)

        Try

            If Not IO.File.Exists(File) Then ' Create a new INI File with "Key=Value""

                My.Computer.FileSystem.WriteAllText(File, ValueName & "=" & Value, False)
                Exit Sub

            Else ' Search line by line in the INI file for the "Key"

                Dim Line_Number As Int64 = 0
                Dim strArray() As String = IO.File.ReadAllLines(File)

                For Each line In strArray
                    If line.ToLower.StartsWith(ValueName.ToLower & "=") Then
                        strArray(Line_Number) = ValueName & "=" & Value
                        IO.File.WriteAllLines(File, strArray) ' Replace "value"
                        Exit Sub
                    End If
                    Line_Number += 1
                Next



                My.Computer.FileSystem.WriteAllText(File, vbNewLine & ValueName & "=" & Value, True) ' Key don't exist, then create the new "Key=Value"

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Load a value.
    ''' </summary>
    ''' <param name="File">The INI file location</param>
    ''' <param name="ValueName">The value name</param>
    ''' <returns>The value itself</returns>
    Public Shared Function Load_Value(ByVal File As String, ByVal ValueName As String) As Object

        If Not IO.File.Exists(File) Then

            Throw New Exception(File & " not found.") ' INI File not found.
            Return Nothing

        Else

            For Each line In IO.File.ReadAllLines(File)
                If line.ToLower.StartsWith(ValueName.ToLower & "=") Then Return line.Split("=").Last
            Next



            Throw New Exception("Key: " & """" & ValueName & """" & " not found.") ' Key not found.
            Return Nothing

        End If

    End Function

    ''' <summary>
    ''' Delete a key.
    ''' </summary>
    ''' <param name="File">The INI file location</param>
    ''' <param name="ValueName">The value name</param>
    Public Shared Sub Delete_Value(ByVal File As String, ByVal ValueName As String)

        If Not IO.File.Exists(File) Then

            Throw New Exception(File & " not found.") ' INI File not found.
            Exit Sub

        Else

            Try

                Dim Line_Number As Int64 = 0
                Dim strArray() As String = IO.File.ReadAllLines(File)

                For Each line In strArray
                    If line.ToLower.StartsWith(ValueName.ToLower & "=") Then
                        strArray(Line_Number) = Nothing
                        Exit For
                    End If
                    Line_Number += 1
                Next

                Array.Copy(strArray, Line_Number + 1, strArray, Line_Number, UBound(strArray) - Line_Number)
                ReDim Preserve strArray(UBound(strArray) - 1)

                My.Computer.FileSystem.WriteAllText(File, String.Join(vbNewLine, strArray), False)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' Sorts the entire INI File.
    ''' </summary>
    ''' <param name="File">The INI file location</param>
    Public Shared Sub Sort_Values(ByVal File As String)

        If Not IO.File.Exists(File) Then

            Throw New Exception(File & " not found.") ' INI File not found.
            Exit Sub

        Else

            Try

                Dim Line_Number As Int64 = 0
                Dim strArray() As String = IO.File.ReadAllLines(File)
                Dim TempList As New List(Of String)

                For Each line As String In strArray
                    If line <> "" Then TempList.Add(strArray(Line_Number))
                    Line_Number += 1
                Next

                TempList.Sort()
                IO.File.WriteAllLines(File, TempList)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

    End Sub

End Class

#End Region