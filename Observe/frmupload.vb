' frmUpload.vb
Imports System.ComponentModel
Imports System.IO
Imports System.Net.Http
Imports System.Diagnostics
Imports System.Threading.Tasks
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class frmUpload
    'Inherits Form


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

    Private uploaderName As String = ""
    Private httpClient As HttpClient
    Private sessionId As String
    Private tokenPollingTimer As Timer
    Private abortUpload As Boolean = False
    Private selectedFolder As String = ""
    Private INIPath As String = Path.Combine(Application.StartupPath, "settings.ini")

    Private BaseUrl As String = uploadURL

    Private Sub frmUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim storedUsername As String = iniread(INIPath, "uploader", "username")
        If Not String.IsNullOrEmpty(storedUsername) Then
            txtUsername.Text = storedUsername
        End If

        sessionId = Guid.NewGuid().ToString("N")
        httpClient = New HttpClient() With {.BaseAddress = New Uri(BaseUrl)}

        tokenPollingTimer = New Timer() With {.Interval = 5000, .Enabled = False}
        AddHandler tokenPollingTimer.Tick, AddressOf PollToken

        Dim authUrl = $"{BaseUrl}auth.php?session={sessionId}"
        Process.Start(New ProcessStartInfo With {.FileName = authUrl, .UseShellExecute = True})

        lblStatus.Text = "Wait for Browser Auth…"
        btnSelectFolder.Enabled = False
        btnAbort.Enabled = False
        tokenPollingTimer.Start()
    End Sub

    Private Async Sub PollToken(sender As Object, e As EventArgs)
        Try
            Dim resp = Await httpClient.GetAsync($"auth.php?action=list_dates&session={sessionId}")
            If resp.IsSuccessStatusCode Then
                tokenPollingTimer.Stop()
                lblStatus.Text = "Auth: OK. Enter a Username and select the folder."
                btnSelectFolder.Enabled = True
            End If
        Catch
        End Try
    End Sub

    Private Sub btnSelectFolder_Click(sender As Object, e As EventArgs) Handles btnSelectFolder.Click
        Dim name = txtUsername.Text.Trim()
        If String.IsNullOrWhiteSpace(name) Then
            MessageBox.Show("Enter a username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Not Regex.IsMatch(name, "^[A-Za-z0-9_-]+$") Then
            MessageBox.Show("The username can only consist of A–Z, a–z, 0–9, underscore (_) and hyphen (-).", "Invalid characters", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        uploaderName = name

        Using fbd As New FolderBrowserDialog
            fbd.InitialDirectory = Application.StartupPath
            fbd.Description = "Select an Snapshot-Folder to Upload:"
            fbd.ShowNewFolderButton = False
            If fbd.ShowDialog() = DialogResult.OK Then
                selectedFolder = fbd.SelectedPath
                lblStatus.Text = $"Ordner selected: {selectedFolder}"
                abortUpload = False
                btnAbort.Enabled = True
                Task.Run(Sub() StartUpload())
            End If
        End Using
    End Sub

    Private Sub btnAbort_Click(sender As Object, e As EventArgs) Handles btnAbort.Click
        abortUpload = True
        lblStatus.Text = "Upload abort."
        DisableControlsDuringUpload(False)
        ' Hinweis: wir lassen die Session bestehen, nur Upload-Schleife wird gestoppt
    End Sub

    Private Async Sub StartUpload()
        If String.IsNullOrEmpty(selectedFolder) Then
            Invoke(Sub() lblStatus.Text = "No folder selected.")
            Return
        End If
        Dim files = Directory.GetFiles(selectedFolder)
        If files.Length = 0 Then
            Invoke(Sub() lblStatus.Text = "Cant find any Files.")
            Return
        End If

        ' Init-Call
        Dim folderName = New DirectoryInfo(selectedFolder).Name
        Dim datePart = folderName.Split("_"c)(0)
        Dim totalFiles = files.Length

        Dim initForm = New MultipartFormDataContent()
        initForm.Add(New StringContent("init"), "action")
        initForm.Add(New StringContent(sessionId), "session")
        initForm.Add(New StringContent(uploaderName), "username")
        initForm.Add(New StringContent(datePart), "date")
        initForm.Add(New StringContent(totalFiles.ToString()), "anzahl")

        Dim initResp = Await httpClient.PostAsync("auth.php", initForm)
        If Not initResp.IsSuccessStatusCode Then
            Dim err = Await initResp.Content.ReadAsStringAsync()
            Invoke(Sub() lblStatus.Text = $"Error at Init: {err}")
            Return
        End If

        Invoke(Sub() DisableControlsDuringUpload(True))
        Dim currentUpload = 0

        For Each filePath In files
            If abortUpload Then Exit For

            Dim fileNameNoExt = Path.GetFileNameWithoutExtension(filePath)
            Dim fileBytes = File.ReadAllBytes(filePath)

            Dim form = New MultipartFormDataContent()
            form.Add(New StringContent("upload"), "action")
            form.Add(New StringContent(sessionId), "session")
            form.Add(New StringContent(uploaderName), "username")
            form.Add(New StringContent(datePart), "date")
            form.Add(New StringContent(fileNameNoExt), "filename")
            form.Add(New StringContent(totalFiles.ToString()), "anzahl")
            Dim fileContent = New ByteArrayContent(fileBytes)
            fileContent.Headers.ContentType = New Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream")
            form.Add(fileContent, "file", Path.GetFileName(filePath))

            Try
                Dim resp = Await httpClient.PostAsync("auth.php", form)
                Dim respStr = (Await resp.Content.ReadAsStringAsync()).Trim()
                If respStr.StartsWith("upload_success:") Then
                    currentUpload += 1
                    UpdateProgress(currentUpload, totalFiles)
                    Invoke(Sub() lblStatus.Text = $"Uploading… ({currentUpload}/{totalFiles})")
                ElseIf respStr = "endOK" Then
                    currentUpload += 1
                    UpdateProgress(currentUpload, totalFiles)
                    Invoke(Sub() lblStatus.Text = "Upload finished.")
                    Exit For
                Else
                    Invoke(Sub() lblStatus.Text = $"Error at Upload: {respStr}")
                    Exit For
                End If
            Catch ex As Exception
                Invoke(Sub() lblStatus.Text = $"Exception: {ex.Message}")
                Exit For
            End Try

            Await Task.Delay(100)
        Next

        Invoke(Sub() DisableControlsDuringUpload(False))
    End Sub

    Private Sub UpdateProgress(current As Integer, total As Integer)
        If progressBar.InvokeRequired Then
            progressBar.Invoke(Sub() progressBar.Value = CInt((current / total) * 100))
        Else
            progressBar.Value = CInt((current / total) * 100)
        End If
    End Sub

    Private Sub DisableControlsDuringUpload(disable As Boolean)
        If btnSelectFolder.InvokeRequired Then
            btnSelectFolder.Invoke(Sub()
                                       btnSelectFolder.Enabled = disable
                                       btnAbort.Enabled = Not disable
                                   End Sub)
        Else
            btnSelectFolder.Enabled = Not disable
            btnAbort.Enabled = disable
        End If
    End Sub

    Private Async Sub frmUpload_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Await httpClient.GetAsync($"auth.php?action=logout&session={sessionId}")
        Catch
        End Try
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Close()
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Me.Close()
    End Sub

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
End Class
