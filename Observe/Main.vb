Imports System.IO
Imports System.Media
Imports System.Net.Http
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Main

    Private _run As RunLockbitsCompressionGZIPModule

#Region "DLL Imports"
    <DllImport("kernel32")>
    Private Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)>
    Private Shared Function WritePrivateProfileString(ByVal lpAppName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Boolean
    End Function
#End Region

#Region "App Controls"
    Private AppWindowpoint As System.Drawing.Point
    Private AppXPos1 As Integer
    Private AppYPos1 As Integer

    Private Function getAppPos()
        AppXPos1 = Control.MousePosition.X - Me.Location.X
        AppYPos1 = Control.MousePosition.Y - Me.Location.Y
        Return 0
    End Function

    Private Function setAppPos(btn As MouseEventArgs)
        If btn.Button = MouseButtons.Left Then
            AppWindowpoint = Control.MousePosition
            AppWindowpoint.X -= (AppXPos1)
            AppWindowpoint.Y -= (AppYPos1)
            Me.Location = AppWindowpoint
        End If
        Return 0
    End Function

    Private Sub PanelMove_MouseDown(sender As Object, e As MouseEventArgs) Handles PanelMove.MouseDown
        getAppPos()
    End Sub

    Private Sub PanelMove_MouseMove(sender As Object, e As MouseEventArgs) Handles PanelMove.MouseMove
        setAppPos(e)
    End Sub

    Private Sub lblTitle_MouseDown(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseDown
        getAppPos()
    End Sub

    Private Sub lblTitle_MouseMove(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseMove
        setAppPos(e)
    End Sub

    Private Sub lblDiscordURL_Click(sender As Object, e As EventArgs) Handles lblDiscordURL.Click
        Dim url As String = DiscordURL
        Dim psi As New ProcessStartInfo() With {
    .FileName = url,
    .UseShellExecute = True
}
        Process.Start(psi)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub


    Public Sub UpdateStatusLabel(message As String)
        If Me.lblSnapTaken.InvokeRequired Then
            Me.lblSnapTaken.Invoke(Sub()
                                       Me.lblSnapTaken.Text = message
                                   End Sub)
        Else
            Me.lblSnapTaken.Text = message
        End If
    End Sub

    Public Function BerechneOrdnerGroesse(ordnerPfad As String) As Long
        Dim gesamtGroesse As Long = 0

        ' Hole alle Dateien im angegebenen Ordner (ohne Unterordner)
        Dim dateien() As String = Directory.GetFiles(ordnerPfad)

        For Each datei In dateien
            Dim info As New FileInfo(datei)
            gesamtGroesse += info.Length
        Next

        Return gesamtGroesse
    End Function

    Public Sub UpdateSize(pfad As String)

        Dim groesseInBytes As Long = BerechneOrdnerGroesse(pfad)

        ' Optional: Umrechnung in ein besser lesbares Format (z. B. KB, MB)
        ' Hier als Beispiel in MB:
        Dim groesseInMB As Double = groesseInBytes / (1024 * 1024)

        TXTSnapSize = $"{groesseInMB:F2} MB"

        If Me.lblSnapSize.InvokeRequired Then
            Me.lblSnapSize.Invoke(Sub()
                                      Me.lblSnapSize.Text = TXTSnapSize
                                  End Sub)
        Else
            Me.lblSnapSize.Text = TXTSnapSize
        End If

    End Sub

    Public Sub UpdateOffsetpoints(message As String)
        If Me.lblOffsetPoints.InvokeRequired Then
            Me.lblOffsetPoints.Invoke(Sub()
                                          Me.lblOffsetPoints.Text = message
                                      End Sub)
        Else
            Me.lblOffsetPoints.Text = message
        End If
    End Sub

    Private Sub btnSnapshotFolder_Click(sender As Object, e As EventArgs) Handles btnSnapshotFolder.Click
        Using fbd As New FolderBrowserDialog
            fbd.Description = "Select the new Snapshot Folder:"
            fbd.ShowNewFolderButton = True

            If fbd.ShowDialog = DialogResult.OK Then
                WriteIniValue("path", "Snapshots", fbd.SelectedPath.ToString, INIPath)
            End If
        End Using
    End Sub


    Private Sub UpdateTimeOffset()
        Try
            Dim ntpTime As DateTime = ntpInstance.GetNetworkTime()
            Dim localTime As DateTime = DateTime.Now
            SyncedTimeOffset = ntpTime - localTime
        Catch ex As Exception
            Debug.WriteLine("Fehler beim Aktualisieren der NTP-Zeit: " & ex.Message)
        End Try
    End Sub

    Private Sub displayTimer_Tick(sender As Object, e As EventArgs) Handles displayTimer.Tick
        lblTime.Text = SyncedTime.ToString("HH:mm:ss | dd.MM.yyyy ")
    End Sub

    ' Synchronisation alle 60 Sekunden
    Private Sub syncTimer_Tick(sender As Object, e As EventArgs) Handles syncTimer.Tick
        UpdateTimeOffset()
    End Sub

    Public ReadOnly Property SyncedTime As DateTime
        Get
            Return DateTime.Now + SyncedTimeOffset
        End Get
    End Property

    Private WithEvents syncTimer As New System.Windows.Forms.Timer()
    Private WithEvents displayTimer As New System.Windows.Forms.Timer()

#End Region

#Region "Funktionen"

    Async Function checkUpdateApp() As Task
        Dim httpClient As New HttpClient()
        Dim server_version_str As String = Await httpClient.GetStringAsync(AppVersionUrl)
        Dim serverVersion As New Version(server_version_str)
        If objVersionApp.CompareTo(serverVersion) < 0 Then
            Dim result As DialogResult = MessageBox.Show("There is an Update avable" & vbCrLf & vbCrLf & "Your App version: " & TXTVersion & vbCrLf & "New App Version: " & server_version_str & vbCrLf & vbCrLf & "You want to Update?", "App Update avable", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If result = DialogResult.Yes Then
                Dim updateEXE As String = "ObserverUpdater.exe"
                Dim ProcID As Integer = Process.GetCurrentProcess().Id
                Dim parameter As String = ProcID.ToString

                Dim startInfo As New ProcessStartInfo()
                startInfo.FileName = updateEXE
                startInfo.Arguments = parameter
                startInfo.UseShellExecute = False
                startInfo.CreateNoWindow = False

                ' Prozess starten
                Process.Start(startInfo)

                Application.Exit()
            Else
            End If

        End If
    End Function

    Async Function checkUpdateUpdater() As Task
        Dim httpClient As New HttpClient()
        Dim updater_version_str As String = iniread(INIPath, "version", "updater")
        Dim server_version_str As String = Await httpClient.GetStringAsync(UpdaterVersionUrl)
        Dim serverVersion As New Version(server_version_str)
        Dim ClientVersion As New Version(updater_version_str)
        If ClientVersion.CompareTo(serverVersion) < 0 OrElse updater_version_str = Nothing Then
            'MsgBox(objVersionUpdater.CompareTo(serverVersion) < 0)
            Dim result As DialogResult = MessageBox.Show("There is an Update avable" & vbCrLf & vbCrLf & "Your Updater version: " & updater_version_str & vbCrLf & "New Updater Version: " & server_version_str & vbCrLf & vbCrLf & "You want to Update?", "Updater Update avable", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If result = DialogResult.Yes Then
                Await DownloadAndReplaceFiles(server_version_str)
            End If

        End If
    End Function

    Private Async Function DownloadAndReplaceFiles(newversion As String) As Task
        Using client As New HttpClient()
            Dim fileName As String = "ObserverUpdater.exe"
            Dim url As String = UpdateExeUrl
            Dim localFilePath As String = Path.Combine(Environment.CurrentDirectory, fileName)
            Try
                Dim response = Await client.GetAsync(url)
                response.EnsureSuccessStatusCode()
                Dim fileBytes = Await response.Content.ReadAsByteArrayAsync()
                File.WriteAllBytes(localFilePath, fileBytes)
                MessageBox.Show("Success: File '" & fileName & "' updated successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                iniwrite(INIPath, "version", "updater", newversion)
            Catch ex As Exception
                MessageBox.Show("Error: Downloading '" & fileName & "' failed: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End Using
    End Function


    Public Function GetIniValue(section As String, key As String, filename As String, Optional defaultValue As String = "") As String
        Dim sb As New StringBuilder(500)
        If GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, filename) > 0 Then
            Return sb.ToString
        Else
            Return defaultValue
        End If
    End Function

    Public Function WriteIniValue(section As String, key As String, value As String, filename As String) As Boolean
        Return WritePrivateProfileString(section, key, value, filename)
    End Function

    Function check_ini_corrupt()
        Dim readout_hllpath = iniread(INIPath, "path", "HLL")
        Dim readout_snappath = iniread(INIPath, "path", "Snapshots")
        Dim readout_resolution_x = iniread(INIPath, "resolution", "HLL_X")
        Dim readout_resolution_y = iniread(INIPath, "resolution", "HLL_Y")
        Dim readout_version = iniread(INIPath, "version", "app")

        If readout_hllpath = Nothing OrElse readout_snappath = Nothing OrElse readout_resolution_x = Nothing OrElse readout_resolution_y = Nothing OrElse readout_version = Nothing Then
            MessageBox.Show("Seems like your settings.ini is corrupt." & vbCrLf & "Check the follow things:" & vbCrLf & "- Check if the HLL settings .ini path is matching the path in the tool settings.ini" & vbCrLf & "- Check if the Snapshot folder exist and matching the path in the tool settings.ini" & vbCrLf & "- Check if the resolution isnt empty in the tool settings.ini" & vbCrLf & "- Check if the app version isnt empty in the tool settings.ini", "settings.ini corrupt", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        Return 0
    End Function

    Function CreateIni()
        Dim usr As String = Environment.UserName
        Dim hll_path_raw As String = "C:\Users\" & usr & "\AppData\Local\HLL\Saved\Config\WindowsNoEditor\GameUserSettings.ini"

        iniwrite(INIPath, "path", "HLL", hll_path_raw)
        iniwrite(INIPath, "path", "Snapshots", Path.Combine(Application.StartupPath, "snapshots"))
        'iniwrite(INIPath, "internal_debug", "timeout_between_save", "5000")
        'iniwrite(INIPath, "internal_debug", "timeout_between_check", "250")
        'iniwrite(INIPath, "internal_debug", "update_timeout", "1")

        iniwrite(INIPath, "version", "app", TXTVersion)
        Return 0
    End Function

    Function InitIni()
        Dim hll_path_raw As String = iniread(INIPath, "path", "HLL")
        Dim hll_resolution_x = iniread(hll_path_raw, "/Script/HLL.ShooterGameUserSettings", "LastUserConfirmedDesiredScreenWidth")
        Dim hll_resolution_y = iniread(hll_path_raw, "/Script/HLL.ShooterGameUserSettings", "LastUserConfirmedDesiredScreenHeight")
        iniwrite(INIPath, "resolution", "HLL_X", hll_resolution_x)
        iniwrite(INIPath, "resolution", "HLL_Y", hll_resolution_y)

        Return 0
    End Function

    Function CheckCreateIni()
        If Not File.Exists(INIPath) Then
            CreateIni()
        Else
            If iniread(INIPath, "version", "app") = Nothing Then
                CreateIni()
            End If
            iniwrite(INIPath, "version", "app", TXTVersion)
        End If
        Return 0
    End Function

    Function CreateSnapshotFolder()
        Dim ordnerPfad As String = Path.Combine(Application.StartupPath, "snapshots")
        If Directory.Exists(ordnerPfad) Then
        Else
            Directory.CreateDirectory(ordnerPfad)
        End If
        Return 0
    End Function

#End Region

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        _run = New RunLockbitsCompressionGZIPModule(Me)

        CreateSnapshotFolder()
        CheckCreateIni()
        InitIni()

        lblVersion.Text = TXTVersion
        lblDiscordURL.Text = DiscordURL

        syncTimer.Interval = 60000
        syncTimer.Start()
        displayTimer.Interval = 1000
        displayTimer.Start()
        UpdateTimeOffset()


        lblOffsetPoints.Text = iniCount(INIPath, "offset_points")

        tbxResolution.Text = iniread(INIPath, "resolution", "HLL_X") & "x" & iniread(INIPath, "resolution", "HLL_Y")
        TXTResolution = tbxResolution.Text


        Dim internal_UPDATER_update = iniread(INIPath, "internal_debug", "update_updater_timeout")
        If internal_UPDATER_update = Nothing Then
            Await checkUpdateUpdater()
        End If

        Dim internal_APP_update = iniread(INIPath, "internal_debug", "update_timeout")
        If internal_APP_update = Nothing Then
            Await checkUpdateApp()
        End If

        check_ini_corrupt()
    End Sub

    Private Sub btnStartTeaching_Click(sender As Object, e As EventArgs) Handles btnStartTeachingInf.Click
        Dim result = MessageBox.Show("How To:  " & vbCrLf & vbCrLf & "1. Check if the displayed HLL Resolution in the Tool is the same as your HLL Resolution" & vbCrLf & "2. Open HLL and join any !EMPTY! Server" & vbCrLf & "3. Spawn (NOT AS COMMANDER!) and open the Map" & vbCrLf & "4. Go back to this Message and press Yes" & vbCrLf & "5. Tab back into the Game and wait" & vbCrLf & "6. If you hear 1x Beep the Teachin started till you hear 2x Beep's then its finished" & vbCrLf & vbCrLf & "This will delete your old Teachin, you want to continue?", "New Teachin", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            iniSECTIONDELETE(INIPath, "offset_points")

            Dim setupModule As New SetupModule
            setupModule.setupSub()

        Else
        End If
    End Sub


    Private runRunLockbitsCompressionGZIPModule As New RunLockbitsCompressionGZIPModule(Me)


    Private Sub cbRunInf_CheckedChanged(sender As Object, e As EventArgs) Handles cbRunInf.CheckedChanged
        If cbRunInf.Checked = True Then
            lblSnapTaken.Text = "0"
            TXTSnapTaken = 0
            Dim snapshotsBase = Path.Combine(Application.StartupPath & "snapshots\")
            Dim folderName = SyncedTime.ToString("dd-MM-yyyy_HH-mm-ss")
            Dim fullFolder = Path.Combine(snapshotsBase, folderName)
            Directory.CreateDirectory(fullFolder)

            runRunLockbitsCompressionGZIPModule.SnapshotsFolder = fullFolder
            runRunLockbitsCompressionGZIPModule.Start()


        Else
            runRunLockbitsCompressionGZIPModule.Stop()

        End If
    End Sub

    Private Sub btnVideoCreatorOpen_Click(sender As Object, e As EventArgs) Handles btnVideoCreatorOpen.Click
        If WebVideoCreatorEnable = True Then
            Dim authUrl = VideoCreatorAuthUrl
            Process.Start(New ProcessStartInfo With {.FileName = authUrl, .UseShellExecute = True})
        Else
            VidCreator.Show()
        End If


    End Sub

    Private Sub btnOpenUploader_Click(sender As Object, e As EventArgs) Handles btnOpenUploader.Click
        frmUpload.Show()
    End Sub

    Private Sub lblChangelog_Click(sender As Object, e As EventArgs) Handles lblChangelog.Click
        changelog.Show()
    End Sub



End Class