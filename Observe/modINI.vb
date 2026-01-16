Imports System.IO

Module modINI
    Private Declare Unicode Function WritePrivateProfileString Lib "kernel32" _
    Alias "WritePrivateProfileStringW" (ByVal lpApplicationName As String,
    ByVal lpKeyName As String, ByVal lpString As String,
    ByVal lpFileName As String) As Int32

    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" _
    Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String,
    ByVal lpKeyName As String, ByVal lpDefault As String,
    ByVal lpReturnedString As String, ByVal nSize As Int32,
    ByVal lpFileName As String) As Int32

    Public Sub writeIni(ByVal iniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamVal As String)
        Dim Result As Integer = WritePrivateProfileString(Section, ParamName, ParamVal, iniFileName)
    End Sub

    Public Function ReadIni(ByVal IniFileName As String, ByVal Section As String, ByVal ParamName As String, ByVal ParamDefault As String) As String
        Dim ParamVal As String = Space$(1024)
        Dim LenParamVal As Long = GetPrivateProfileString(Section, ParamName, ParamDefault, ParamVal, Len(ParamVal), IniFileName)
        ReadIni = Left$(ParamVal, LenParamVal)
    End Function


    Public Function DeleteIniSection(ByVal iniFileName As String, ByVal Section As String) As Boolean
        Dim result As Integer = WritePrivateProfileString(Section, vbNullString, vbNullString, iniFileName)
        Return result <> 0
    End Function


    Function iniread(Path As String, Section As String, Key As String)
        Dim c As String = ReadIni(Path, Section, Key, "")
        Return c
    End Function

    Function iniwrite(Path As String, Section As String, Key As String, value As String)
        writeIni(Path, Section, Key, value)
        Return 0
    End Function

    Function iniSECTIONDELETE(Path As String, Section As String)
        DeleteIniSection(Path, Section)
        Return 0
    End Function

    Public Function iniCheck5ormore(ByVal iniFileName As String, ByVal section As String) As Boolean
        Dim buffer As String = New String(" "c, 2048)
        Dim size As Integer = GetPrivateProfileString(section, vbNullString, "", buffer, buffer.Length, iniFileName)

        ' Die Keys sind durch Nullzeichen getrennt.
        Dim keys() As String = buffer.Substring(0, size).Split(ControlChars.NullChar, StringSplitOptions.RemoveEmptyEntries)

        Dim count As Integer = 0
        For Each key As String In keys
            Dim value As String = ReadIni(iniFileName, section, key, "")
            If value <> "" Then
                count += 1
            End If
            If count >= 5 Then
                Return True
            End If
        Next

        Return False
    End Function

    Public Function iniCount(ByVal iniFileName As String, ByVal section As String) As String
        Dim buffer As String = New String(" "c, 2048)
        Dim size As Integer = GetPrivateProfileString(section, vbNullString, "", buffer, buffer.Length, iniFileName)

        ' Die Keys sind durch Nullzeichen getrennt.
        Dim keys() As String = buffer.Substring(0, size).Split(ControlChars.NullChar, StringSplitOptions.RemoveEmptyEntries)

        Dim count As Integer = 0
        For Each key As String In keys
            Dim value As String = ReadIni(iniFileName, section, key, "")
            If value <> "" Then
                count += 1
            End If
        Next

        Return (count + 1).ToString
    End Function

End Module
